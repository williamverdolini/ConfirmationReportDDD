using CR.Application.Abstractions.Models;
using CR.Application.Workers;
using CR.Infrastructure;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CR.Application.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthWorker worker;

        public AccountController(IAuthWorker worker)
        {
            Contract.Requires<ArgumentNullException>(worker != null, "IAuthWorker worker");
            this.worker = worker;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterUserViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegisterResultViewModel result = await worker.RegisterUser(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [Authorize]
        [ResponseType(typeof(UserViewModel))]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            var user = await worker.GetUserInfo(User.Identity.GetUserId());
            if (user == null)
                return NotFound();
            return Ok(user);
        }        

        private IHttpActionResult GetErrorResult(RegisterResultViewModel result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
