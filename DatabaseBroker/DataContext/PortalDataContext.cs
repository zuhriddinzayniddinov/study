using Entity.Enum;
using Entity.Models;
using Entity.Models.Common;
using Entity.Models.Learning;
using Entity.Models.StaticFiles;
using Entitys.Models;
using Microsoft.EntityFrameworkCore;
using Author = Entity.Models.Learning.Author;
using Exam = Entity.Models.Learning.Exam;
using Quiz = Entity.Models.Learning.Quiz;

namespace DatabaseBroker.DataContext;

public class PortalDataContext : DbContext
{
    public PortalDataContext(DbContextOptions<PortalDataContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }


    protected void TrackActionsAt()
    {
        foreach (var entity in this.ChangeTracker.Entries()
                     .Where(x => x.State == EntityState.Added && x.Entity is AuditableModelBase<int>))
        {
            var model = (AuditableModelBase<int>)entity.Entity;
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = model.CreatedAt;
        }

        foreach (var entity in this.ChangeTracker.Entries()
                     .Where(x => x.State == EntityState.Modified && x.Entity is AuditableModelBase<int>))
        {
            var model = (AuditableModelBase<int>)entity.Entity;
            model.UpdatedAt = DateTime.Now;
        }
    }

    public override int SaveChanges()
    {
        TrackActionsAt();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        TrackActionsAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        TrackActionsAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        TrackActionsAt();
        return base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Configuring all MultiLanguage fields over entities
        var multiLanguageFields = modelBuilder
            .Model
            .GetEntityTypes()
            .SelectMany(x => x.ClrType.GetProperties())
            .Where(x => x.PropertyType == typeof(MultiLanguageField));

        foreach (var multiLanguageField in multiLanguageFields)
            modelBuilder
                .Entity(multiLanguageField.DeclaringType!)
                .Property(multiLanguageField.PropertyType, multiLanguageField.Name)
                .HasColumnType("jsonb");

        modelBuilder
            .Entity<SignMethod>()
            .HasOne(x => x.User)
            .WithMany(x => x.SignMethods)
            .HasForeignKey(x => x.UserId);

        modelBuilder
            .Entity<SignMethod>()
            .HasDiscriminator(x => x.Type)
            .HasValue<ESISignMethod>(SignMethods.ESI)
            .HasValue<OneIDSignMethod>(SignMethods.OneId)
            .HasValue<DefaultSignMethod>(SignMethods.Normal);
        
        modelBuilder.Entity<SimpleQuestion>()
            .Property(x => x.Options)
            .HasColumnType("jsonb");
        
        modelBuilder
            .Entity<Question>()
            .HasDiscriminator(x => x.QuestionType)
            .HasValue<SimpleQuestion>(QuestionTypes.Simple)
            .HasValue<WrittenQuestion>(QuestionTypes.Written);
        
        modelBuilder.Entity<SimpleQuestionInExam>()
            .Property(x => x.Options)
            .HasColumnType("jsonb");
        
        modelBuilder
            .Entity<QuestionInExam>()
            .HasDiscriminator(x => x.QuestionType)
            .HasValue<SimpleQuestionInExam>(QuestionTypes.Simple)
            .HasValue<WrittenQuestionInExam>(QuestionTypes.Written);

        modelBuilder
            .Entity<User>()
            .HasOne(x => x.UserCerifiticate)
            .WithOne(x => x.Owner)
            .HasForeignKey<UserCertificate>(x => x.OwnerId);

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Courses)
            .WithOne(a => a.Author)
            .HasForeignKey(c => c.AuthorId);
        
        modelBuilder.Entity<Author>()
            .HasMany(a => a.ShortVideos)
            .WithOne(a => a.Author)
            .HasForeignKey(c => c.AuthorId);
        
        modelBuilder.Entity<Author>()
            .HasMany(a => a.SeminarVideos)
            .WithOne(a => a.Author)
            .HasForeignKey(c => c.AuthorId);
        
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Articles)
            .WithOne(a => a.Author)
            .HasForeignKey(c => c.AuthorId);
    }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Structure> Structures { get; set; }
    public DbSet<StructurePermission> StructurePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TokenModel> Tokens { get; set; }
    public DbSet<SignMethod> UserSignMethods { get; set; }
    public DbSet<UserCertificate> ESigns { get; set; }
    public DbSet<StaticFile> StaticFiles { get; set; }
    public DbSet<Article> Article { get; set; }
    public DbSet<SeminarVideo> VideoCourse { get; set; }
    public DbSet<Category> VideoCourseCategory { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<ShortVideo> ShortVideo { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<VideoOfCourse> VideoOfCourses { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<QuestionInExam> QuestionInExams { get; set; }
}