using ActiveDirectoryExplorer.Models;
using ActiveDirectoryExplorer.Models.DTOs;
using ActiveDirectoryExplorer.Models.Enums;
using ActiveDirectoryExplorer.Repositories;
using ActiveDirectoryExplorer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ActiveDirectoryExplorer.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly ValidationService _validationService;

        public UserController(UserRepository userRepository, ValidationService validationService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }

        /// <summary>
        /// Retrieves relevant data of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="domain">The Active Directory domain.</param> 
        [HttpGet("{userId}")]
        public IActionResult GetUser(string userId, string domain)
        {
            GetDataDTO req = new GetDataDTO {
                Name = userId,
                Domain = domain,
                Type = "user"
            };

            _validationService.IsValidDomain(req.Domain);

            User user = _userRepository.Get(req);

            if (user == null)
            {
                return NotFound($"{userId} not found on domain {domain}.");
            }

            //JObject json = JObject.Parse(JsonConvert.SerializeObject(user));

            //return Ok(JsonConvert.SerializeObject(user, Formatting.Indented));
            return Ok(user);
            //return Ok(json);
        }

        /// <summary>
        /// Adds the specified user on the specidied group.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="groupId">The group ID.</param>
        /// <param name="domain">The Active Directory domain.</param> 
        [HttpPatch("add")]
        public IActionResult AddUser(string userId, string groupId, string domain)
        {
            PatchDataDTO req = new PatchDataDTO
            {
                Name = userId,
                Group = groupId,
                Domain = domain,
                Type = "user",
                Action = ActiveDirectoryOperation.ADD
            };

            _userRepository.Update(req);

            return Ok($"{userId} added to the group {groupId}.");
        }

        /// <summary>
        /// Removes the specified user of the specidied group.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="groupId">The group ID.</param>
        /// <param name="domain">The Active Directory domain.</param> 
        [HttpPatch("remove")]
        public IActionResult RemoveUser(string userId, string groupId, string domain)
        {
            PatchDataDTO req = new PatchDataDTO
            {
                Name = userId,
                Group = groupId,
                Domain = domain,
                Type = "computer",
                Action = ActiveDirectoryOperation.REMOVE
            };

            _userRepository.Update(req);

            return Ok($"{userId} removed from the group {groupId}.");
        }
    }
}
