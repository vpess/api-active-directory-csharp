using ActiveDirectoryExplorer.Models.DTOs;
using ActiveDirectoryExplorer.Models;
using ActiveDirectoryExplorer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ActiveDirectoryExplorer.Controllers
{
    [ApiController]
    [Route("api/groups")]
    [Produces("application/json")]
    public class GroupController : ControllerBase
    {
        private readonly GroupRepository _groupRepository;

        public GroupController(GroupRepository groupRepository) {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
        }

        /// <summary>
        /// Retrieves relevant data of the specified group.
        /// </summary>
        /// <param name="groupId">The group ID.</param>
        /// <param name="domain">The Active Directory domain.</param> 
        [HttpGet("{groupId}")]
        public IActionResult GetGroup(string groupId, string domain)
        {
            GetDataDTO req = new GetDataDTO
            {
                Name = groupId,
                Domain = domain,
                Type = "group"
            };

            Group group = _groupRepository.Get(req);

            if (group == null)
            {
                return NotFound($"'{groupId}' not found on domain {domain}.");
            }

            return Ok(group);
        }
    }
}
