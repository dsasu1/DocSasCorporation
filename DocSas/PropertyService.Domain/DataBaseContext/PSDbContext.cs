using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PropertyService.Domain.DataEntities;
using PropertyService.Domain.ModelView;

namespace PropertyService.Domain.DataBaseContext
{
    /// <summary>
    /// Class PSDbContext.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class PSDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PSDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PSDbContext(DbContextOptions<PSDbContext> options) : base(options)
        {

        }

        //Tables
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestion { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set;}
        public DbSet<Language> Languages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<LogException> LogExceptions { get; set; }
        public DbSet<ServiceCodeTrack> ServiceCodeTracks { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
        public DbSet<PropertyInformation> PropertyInformations { get; set; }
        public DbSet<CommentCard> CommentCards { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<TenantUnit> TenantUnits { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<OperationHour> OperationHours { get; set; }
        public DbSet<PropertyEvent> PropertyEvents { get; set; }
        public DbSet<PropertyReview> PropertyReviews { get; set; }
        public DbSet<NewsPost> NewsPosts { get; set; }
        public DbSet<AppPage> AppPages { get; set; }
        public DbSet<AvailableRole> AvailableRoles { get; set; }
        public DbSet<PageAccess> PageAccesses { get; set; }
        public DbSet<PropertyAccess> PropertyAccesses { get; set; }
        public DbSet<PropertyEnabledPage> PropertyEnabledPages { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<ServiceApiKey> ServiceApiKeys { get; set; }
        public DbSet<NotificationViewTrack> NotificationViewTracks { get; set; }

        //Procedures
        public DbSet<CommentCardVM> CommentCardVMS { get; set; }
        public DbSet<ServiceRequestVM> ServiceRequestVMS { get; set; }
        public DbSet<ReviewVM> ReviewVMS { get; set; }
        public DbSet<PostVM> PostVMS { get; set; }
        public DbSet<EventVM> EventVMS { get; set; }
        public DbSet<StaffRoleVM> StaffRoleVMS { get; set; }
        public DbSet<ResidentsVM> ResidentsVMS { get; set; }
        public DbSet<NotificationVM> NotificationVMS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyInformation>()
            .HasOne(x => x.ZipCode).WithMany().HasForeignKey(x=> x.ZipId);
            //modelBuilder.Entity<PropertyInformation>().HasOne(b => b.ZipCode).WithOne().HasForeignKey<PropertyInformation>(c => c.ZipId);
            //  base.OnModelCreating(modelBuilder);
            //HasPrincipalKey<ZipCode>(p=> p.Id).
        }
    } 
}
