using LeaveMGMTApi.Helpers;
using LeaveMGMTApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveMGMTApi.Data
{
    public class DBContext: DbContext
    {
            public DBContext(DbContextOptions<DBContext> options) : base(options)
            {

            }
        public  DbSet<Users> Users { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {

                entity.ToTable("TBL_USERS");

                entity.HasKey(e => e.UserId)
                    .HasName("USM_ID");


                entity.Property(e => e.UserFullName)
                    .HasColumnName("USM_FULL_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Emailid)
                    .HasColumnName("USM_EMAIL")
                    .HasMaxLength(50);

                entity.Property(e => e.MobileNo)
                    .HasColumnName("USM_MOBILE")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpId)
                    .HasColumnName("USM_EMP_ID")
                    .HasMaxLength(25);

                entity.Property(e => e.Pwd)
                    .HasColumnName("USM_PWD")
                    .HasMaxLength(250);

                entity.Property(e => e.LastLoginDate)
                    .HasColumnName("USM_LAST_LOGIN_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.Status)
                  .HasColumnName("USM_STATUS")
                  .HasColumnType("NUMBER");

                entity.Property(e => e.CreatedDt)
                  .HasColumnName("USM_CREATED_DATE")
                  .HasColumnType("DATE");

                entity.Property(e => e.CreatedBy)
                  .HasColumnName("USM_CREATED_BY");

            });

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.ToTable("LeaveRequests"); 
                entity.HasKey(lr => lr.Id);
                entity.Property(lr => lr.StartDate).IsRequired();
                entity.Property(lr => lr.EndDate).IsRequired();
                entity.Property(lr => lr.Reason).HasMaxLength(200);
            });
            base.OnModelCreating(modelBuilder);
        }

     

       }
}
