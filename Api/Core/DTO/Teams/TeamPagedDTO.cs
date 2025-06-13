using System.Collections.Generic;

namespace Core.DTO.Teams
{
    public class TeamPagedDTO
    {
        public int PageCount { get; set; }
        public int TotalItems { get; set; }
        public List<TeamDTO> Data { get; set; } = new();
    }
}