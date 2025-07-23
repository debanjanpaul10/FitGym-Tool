// *********************************************************************************
//	<copyright file="SqlDbContext.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The SQL DB Context Class.</summary>
// *********************************************************************************

using Microsoft.EntityFrameworkCore;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.Mapping;
using System.Diagnostics.CodeAnalysis;

namespace FitGymTool.Persistence.Adapters;

/// <summary>
/// The SQL DB context class.
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
[ExcludeFromCodeCoverage]
public class SqlDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SqlDbContext"/> class.
    /// </summary>
    /// <remarks>
    /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information and examples.
    /// </remarks>
    public SqlDbContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the member details.
    /// </summary>
    /// <value>
    /// The member details.
    /// </value>
    public DbSet<MemberDetails> MemberDetails { get; set; }

    /// <summary>
    /// Gets or sets the fees status.
    /// </summary>
    /// <value>
    /// The fees status.
    /// </value>
    public DbSet<FeesStatus> FeesStatus { get; set; }

    /// <summary>
    /// Gets or sets the fees payment history.
    /// </summary>
    /// <value>
    /// The fees payment history.
    /// </value>
    public DbSet<FeesPaymentHistory> FeesPaymentHistory { get; set; }

    /// <summary>
    /// Gets or sets the fees structure.
    /// </summary>
    /// <value>
    /// The fees structure.
    /// </value>
    public DbSet<FeesStructure> FeesStructure { get; set; }

    /// <summary>
    /// Gets or sets the current month fees and revenue status (for raw SQL queries).
    /// </summary>
    public DbSet<CurrentMonthFeesAndRevenueStatus> CurrentMonthFeesAndRevenueStatus { get; set; }

    /// <summary>
    /// Gets or sets the current member fees status.
    /// </summary>
    /// <value>
    /// The current member fees status.
    /// </value>
    public DbSet<CurrentMembersFeesStatus> CurrentMemberFeesStatus { get; set; }

    /// <summary>
    /// Gets or sets the member payment history data.
    /// </summary>
    /// <value>
    /// The member payment history data.
    /// </value>
    public DbSet<MemberPaymentHistoryData> MemberPaymentHistoryData { get; set; }

    /// <summary>
    /// Gets or sets the bug report data.
    /// </summary>
    /// <value>
    /// The bug report data.
    /// </value>
    public DbSet<BugReportData> BugReportData { get; set; }

    #region Mapping Entities

    /// <summary>
    /// Gets or sets the membership status mapping.
    /// </summary>
    /// <value>
    /// The membership status mapping.
    /// </value>
    public DbSet<MembershipStatusMapping> MembershipStatusMapping { get; set; }

    /// <summary>
    /// Gets or sets the fees payment status mapping.
    /// </summary>
    /// <value>
    /// The fees payment status mapping.
    /// </value>
    public DbSet<FeesPaymentStatusMapping> FeesPaymentStatusMapping { get; set; }

    /// <summary>
    /// Gets or sets the fees duration mapping.
    /// </summary>
    /// <value>
    /// The fees duration mapping.
    /// </value>
    public DbSet<FeesDurationMapping> FeesDurationMapping { get; set; }

    /// <summary>
    /// Gets or sets the bug item status mapping.
    /// </summary>
    /// <value>
    /// The bug item status mapping.
    /// </value>
    public DbSet<BugItemStatusMapping> BugItemStatusMapping { get; set; }

    /// <summary>
    /// Gets or sets the bug severity mapping.
    /// </summary>
    /// <value>
    /// The bug severity mapping.
    /// </value>
    public DbSet<BugSeverityMapping> BugSeverityMapping { get; set; }

    #endregion

    /// <summary>
    /// Override this method to configure the database (and other options) to be used for this context.
    /// This method is called for each instance of the context that is created.
    /// The base implementation does nothing.
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
    /// typically define extension methods on this object that allow you to configure the context.</param>
    /// <remarks>
    /// <para>
    /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
    /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
    /// the options have already been set, and skip some or all of the logic in
    /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information and examples.
    /// </para>
    /// </remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    /// <summary>
    /// Override this method to further configure the model that was discovered by convention from the entity types
    /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    /// and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
    /// define extension methods on this object that allow you to configure aspects of the model that are specific
    /// to a given database.</param>
    /// <remarks>
    /// <para>
    /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    /// then this method will not be run. However, it will still run when creating a compiled model.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
    /// examples.
    /// </para>
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemberDetails>().HasKey(m => m.MemberId);
        modelBuilder.Entity<MembershipStatusMapping>().HasKey(ms => ms.Id);
        modelBuilder.Entity<MemberDetails>()
            .HasOne(m => m.MembershipStatusMapping)
            .WithMany()
            .HasForeignKey(m => m.MembershipStatusId)
            .HasPrincipalKey(ms => ms.Id);

        modelBuilder.Entity<FeesStructure>().HasKey(fs => fs.Id);
        modelBuilder.Entity<FeesDurationMapping>().HasKey(fdm => fdm.Id);
        modelBuilder.Entity<FeesStructure>()
            .HasOne(fs => fs.FeesDurationMapping)
            .WithMany()
            .HasForeignKey(fs => fs.FeesDurationId)
            .HasPrincipalKey(fdm => fdm.Id);

        modelBuilder.Entity<CurrentMonthFeesAndRevenueStatus>().HasNoKey();
        modelBuilder.Entity<CurrentMembersFeesStatus>().HasNoKey();
        modelBuilder.Entity<MemberPaymentHistoryData>().HasNoKey();
    }
}
