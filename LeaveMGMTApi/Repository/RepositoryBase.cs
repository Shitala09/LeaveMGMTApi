﻿using LeaveMGMTApi.Data;
using LeaveMGMTApi.Interfaces;
using LeaveMGMTApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveMGMTApi.Repository
{
    public class RepositoryBase: IReposaryBase
    {
        private readonly DBContext _context;
        public RepositoryBase(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> Users()
        {
            List<Users> users = await _context.Users.ToListAsync();
            return users;

        }
    }
}
