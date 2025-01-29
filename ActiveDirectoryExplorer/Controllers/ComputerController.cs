using ActiveDirectoryExplorer.Models;
using ActiveDirectoryExplorer.Models.DTOs;
using ActiveDirectoryExplorer.Models.Enums;
using ActiveDirectoryExplorer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ActiveDirectoryExplorer.Controllers
{
    [ApiController]
    [Route("api/computers")]
    [Produces("application/json")]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerRepository _computerRepository;

        public ComputerController(ComputerRepository computerRepository)
        {
            _computerRepository = computerRepository ?? throw new ArgumentNullException(nameof(computerRepository));
        }

        /// <summary>
        /// Retrieves relevant data of the specified computer.
        /// </summary>
        /// <param name="computerId">The computer ID.</param>
        /// <param name="domain">The Active Directory domain.</param> 
        [HttpGet("{computerId}")]
        public IActionResult GetComputer(string computerId, string domain)
        {
            GetDataDTO req = new GetDataDTO
            {
                Name = computerId,
                Domain = domain,
                Type = "computer"
            };

            Computer computer = _computerRepository.Get(req);

            if (computer == null) {
                return NotFound($"{computerId} not found on domain {domain}.");
            }

            return Ok(computer);
        }

        /// <summary>
        /// Adds the specified computer on the specidied group.
        /// </summary>
        /// <param name="computerId">The computer ID.</param>
        /// <param name="groupId">The group ID.</param>
        /// <param name="domain">The Active Directory domain.</param> 
        [HttpPatch("add")]
        public IActionResult AddComputer(string computerId, string groupId, string domain)
        {
            PatchDataDTO req = new PatchDataDTO
            {
                Name = computerId,
                Group = groupId,
                Domain = domain,
                Type = "computer",
                Action = ActiveDirectoryOperation.ADD
            };

            _computerRepository.Update(req);

            return Ok($"{computerId} added to the group {groupId}.");
        }

        /// <summary>
        /// Removes the specified computer of the specidied group.
        /// </summary>
        /// <param name="computerId">The computer ID.</param>
        /// <param name="groupId">The group ID.</param>
        /// <param name="domain">The Active Directory domain.</param> 
        [HttpPatch("remove")]
        public IActionResult RemoveComputer(string computerId, string groupId, string domain)
        {
            PatchDataDTO req = new PatchDataDTO
            {
                Name = computerId,
                Group = groupId,
                Domain = domain,
                Type = "computer",
                Action = ActiveDirectoryOperation.REMOVE
            };

            _computerRepository.Update(req);

            return Ok($"{computerId} removed from the group {groupId}.");
        }
    }
}
