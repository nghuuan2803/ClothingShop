﻿namespace Shared.Requests
{
    public class UpdateCategoryReq
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
