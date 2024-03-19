
using LaptopStore.Models;
using LaptopStore.Models.DataModels;
using LaptopStore.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace LaptopStore.Controllers
{
	public class AccessController : Controller
	{
		LaptopStoreContext db = new LaptopStoreContext();
		[HttpGet]
		public IActionResult Login()
		{
            TempData["msg"] = "";
            if (HttpContext.Session.GetString("username") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public IActionResult Login(Account account)
		{
			if (HttpContext.Session.GetString("username") == null)
			{
				var u = db.Accounts.Where(u => u.Username.Equals(account.Username) && u.Password.Equals(account.Password)).FirstOrDefault();
				
				if (u != null)
				{
					HttpContext.Session.SetString("username", account.Username.ToString());
					return RedirectToAction("Index", "Home");
				}
				else
				{
                    TempData["msg"] = "Invalid username or password";
					return View();
                }
			}
			return View();
		}

		[HttpGet]
		public IActionResult Signup()
		{
            TempData["msg"] = "";
            TempData["err"] = "";
            if (HttpContext.Session.GetString("username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

		[HttpPost]
		public IActionResult Signup(Account account)
		{
			TempData["msg"] = "";
			TempData["err"] = "";
            if (HttpContext.Session.GetString("username") == null)
            {
				var email = db.Accounts.Where(u=>u.Email.Equals(account.Email)).FirstOrDefault();
                string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                bool isMatchE = Regex.IsMatch(account.Email.ToString(), emailRegex);

                if (!isMatchE)
                {
                    TempData["Err"] = "Email is invalid";
                    return View();
                }
                if (email != null)
				{
					TempData["Err"] = "Email is already exists";
					return View();
				}
				var username = db.Accounts.Where(u=>u.Username.Equals(account.Username)).FirstOrDefault();
                string usernameRegex = @"^[^\s]{6,20}$";
                bool isMatchU = Regex.IsMatch(account.Username.ToString(), usernameRegex);
				if (!isMatchU)
				{
                    TempData["Err"] = "Username must be at least 6 character and should not contain any whitespace characters";
                    return View();
                }
                if (username != null)
				{
                    TempData["Err"] = "Username is already exists";
                    return View();
                }
                var pass = account.Password.ToString();
                string passwordRegex = "^(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\\S+$).{8,}$";
                bool isMatch = Regex.IsMatch(pass, passwordRegex);
				if (!isMatch)
				{
					TempData["Err"] = "Password at least 8 characrter , 1 uppercase , 1 special character and should not contain any whitespace characters.  !!!";
					return View();
				}
                if (!account.rePass.Equals(account.Password))
                {
                    TempData["Err"] = "Repassword is not the same as password  !!!";
                    return View();
                }
				account.CreateAt = DateTime.Now;
				account.RoleId = 2;
				account.Enabled = true;
				account.Verify = false;
                db.Accounts.Add(account);
				
				db.SaveChanges();
				TempData["msg"] = "Sign up successfully";
                return RedirectToAction("Login", "Access");
                
            }
            return View();
        }

		public IActionResult ChangePassword()
		{
            if(HttpContext.Session.GetString("username") == null)
            {
                TempData["msg"] = "You are not logged into the system";
                return RedirectToAction("Login");
            }
            

            return View();
		}
        
        
		[HttpPost]
		public IActionResult ChangePassword(string oldPassword, string newPassword, string reNewPassword)
		{
            
            if (HttpContext.Session.GetString("username") == null)
            {
                TempData["msg"] = "You are not logged into the system";
                return RedirectToAction("Login");
            }
            var username = HttpContext.Session.GetString("username");
            var user = db.Accounts.Find(username);
            if(user == null)
            {
                TempData["msg"] = "Account not found";
                return RedirectToAction("Login");
            }
            if (!oldPassword.Equals(user.Password))
            {
                TempData["Err"] = "Old password is not true";
                return View();
            }
            if (!newPassword.Equals(reNewPassword))
            {
                TempData["Err"] = "The re-enter password is not the same as the new password";
                return View();
            }
            string passwordRegex = "^(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\\S+$).{8,}$";
            bool isMatch = Regex.IsMatch(newPassword, passwordRegex);
            if (!isMatch)
            {
                TempData["Err"] = "Password at least 8 characrter , 1 uppercase , 1 special character and should not contain any whitespace characters.  !!!";
                return View();
            }
            user.Password = newPassword;
            db.Accounts.Update(user);
            db.SaveChanges();
            TempData["Err"] = "Change password successfully";
            return RedirectToAction("ChangePassword");
		}

		public IActionResult ForgotPassword()
		{
			TempData["err"] = "";

            return View();
		}
		[HttpPost]
		public IActionResult ForgotPassword(string email)
		{
			TempData["err"] = "";

            if (email == null)
			{
				TempData["err"] = "Please input an email";
				return View();
			}
			var account = db.Accounts.FirstOrDefault(x=> x.Email.Equals(email));
			if(account == null)
			{
				TempData["err"] = "Email is not existed";
				return View();
			}

            try
            {
                Random rand = new Random();
                int otp = rand.Next(100000, 1000000);
                var from = new MailAddress("minh8adv@gmail.com");
                var to = new MailAddress(email.ToString());
                const string frompass = "fhiy zdcn cwys xapw";
                var subject = "OTP code";
                var body = "Your OTP code: " + otp.ToString();

                var gmail = "minh8adv@gmail.com";
                var password = "trinhquangminh2k3";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Address, frompass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                })
                {
                    smtp.Send(message);
                }
                account.OtpCode = otp.ToString();
                account.TimeOtpCreated = DateTime.Now;
                db.Accounts.Update(account);
                db.SaveChanges();
                HttpContext.Session.SetString("emailForgot", email);
                return RedirectToAction("ForgotPasswordOtp");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
		}
        public IActionResult ForgotPasswordOtp()
        {
            ViewBag.Email = HttpContext.Session.GetString("emailForgot").ToString();
            TempData["Err"] = ViewBag.Email;
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPasswordOtp(string email, string otpCode)
        {
            if (otpCode == null)
            {
                TempData["Err"] = "Otpcode is not null";
            }
            
            var account = db.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            if (account == null)
            {
                TempData["err"] = "Email is not existed";
                return View();
            }
            TimeSpan timeDifference = (TimeSpan)(DateTime.Now - account.TimeOtpCreated);

            
            if (timeDifference.TotalMinutes > 5)
            {
                TempData["Err"] = "Otpcode is expired";
                return RedirectToAction("ForgotPassword");
            }
            if (!otpCode.Equals(account.OtpCode))
            {
                TempData["Err"] = "Otpcode is wrong";
                return RedirectToAction("ForgotPasswordOtp");
            }
            return RedirectToAction("ForgotPasswordEnd");
        }

        public IActionResult ForgotPasswordEnd()
        {
            ViewBag.Email = HttpContext.Session.GetString("emailForgot").ToString();
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPasswordEnd(string newPassword, string rePassword, string email)
        {
            if (newPassword == null || rePassword == null)
            {
                TempData["Err"] = "New password is not null";
                return RedirectToAction("ForgotPasswordEnd");
            }
            string passwordRegex = "^(?=.*[A-Z])(?=.*[@#$%^&+=!])(?=\\S+$).{8,}$";
            bool isMatch = Regex.IsMatch(newPassword, passwordRegex);
            if (!isMatch)
            {
                TempData["Err"] = "Password at least 8 characrter , 1 uppercase , 1 special character and should not contain any whitespace characters.  !!!";
                return RedirectToAction("ForgotPasswordEnd");
            }
            var account = db.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            if (account == null)
            {
                TempData["err"] = "Email is not existed";
                return RedirectToAction("ForgotPasswordEnd");
            }
            if (!newPassword.Equals(rePassword))
            {
                TempData["Err"] = "Repassword is not the same as password  !!!";
                return RedirectToAction("ForgotPasswordEnd");
            }
            account.Password = newPassword;
            db.Accounts.Update(account);
            db.SaveChanges();
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("emailForgot");
            TempData["msg"] = "Get new password successfully";
            return RedirectToAction("Login");
        }

		[HttpGet]
		public async Task<IActionResult> TestSendMail()
		{
			try
			{
                Random rand = new Random();
                int otp = rand.Next(100000, 1000000);
                var from = new MailAddress("minh8adv@gmail.com");
                var to = new MailAddress("MinhTQHE171380@fpt.edu.vn");
                const string frompass = "fhiy zdcn cwys xapw";
                var subject = "OTP code";
                var body = "Your OTP code: " + otp.ToString();

                var gmail = "minh8adv@gmail.com";
                var password = "trinhquangminh2k3";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Address, frompass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                })
                {
                    smtp.Send(message);
                }
				return Ok();
            }
            catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
        }
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("username");
			return RedirectToAction("Login", "Access");
		}
	}
}
