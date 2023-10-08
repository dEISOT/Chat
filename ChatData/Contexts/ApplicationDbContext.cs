using ChatData.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatData.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Set PKs
            modelBuilder
              .Entity<User>()
              .HasKey(u => u.Id);

            modelBuilder
               .Entity<Message>()
               .HasKey(m => m.Id);

            modelBuilder
               .Entity<Conversation>()
               .HasKey(c => c.Id);

            modelBuilder
                .Entity<Member>()
                .HasKey(cu => new { cu.UserId, cu.ConversationId });

            modelBuilder
                .Entity<Member>()
                .HasIndex(cu => new { cu.UserId, cu.ConversationId }).IsUnique();

            //Relation between Users and Conversations (many to many with 3d table ConversationUser)
            modelBuilder.Entity<Member>()
                .HasOne<User>(cu => cu.User)
                .WithMany(u => u.Members)
                .HasForeignKey(cu => cu.UserId);

            modelBuilder.Entity<Member>()
                .HasOne<Conversation>(cu => cu.Conversation)
                .WithMany(u => u.Members)
                .HasForeignKey(cu => cu.ConversationId);

            //Relation between Conversation and Messages (one to many) with cascade deleting
            modelBuilder.Entity<Message>()
                .HasOne<Conversation>(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConverstaionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
               new User[]
               {
                    new User{Id = new Guid("2f48bed2-c5ba-48c9-aca3-1639f75ada10"), Name = "User1"},
                    new User{Id = new Guid("a6b022e2-53e0-4dfe-943a-73cb99ebd5ec"), Name = "User2"},
                    new User{Id = new Guid("5331d2f7-2913-499b-abcf-2ebc004e7431"), Name = "User3"},
                    new User{Id = new Guid("b130bed0-9cb8-4266-93c2-9fc2d2d834c4"), Name = "User4"},
                    new User{Id = new Guid("529d88c3-49ae-4f0b-80e3-f7c2acf9589c"), Name = "User5"},
                    new User{Id = new Guid("28c64207-66c6-416c-ac52-67de26fa7968"), Name = "User6"},
                    new User{Id = new Guid("cda31851-186e-484e-a113-d7d349ddffa4"), Name = "User7"},
               });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
