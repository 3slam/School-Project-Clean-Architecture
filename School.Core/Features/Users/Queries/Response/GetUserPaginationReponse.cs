﻿namespace School.Core.Features.Users.Queries.Response
{
    public class GetUserPaginationReponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
