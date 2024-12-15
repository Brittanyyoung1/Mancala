using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS.Mancala.PL.Entities;

namespace SS.Mancala.PL.Data
{
    public class MancalaEntities : DbContext
    {

        // Guid arrays for seeding
        Guid[] playerId = new Guid[4];
        Guid[] gameId = new Guid[4];
        Guid[] moveId = new Guid[3];
        Guid[] pitId = new Guid[14];
        Guid[] userId = new Guid[4];

        public virtual DbSet<tblPlayer> tblPlayers { get; set; }
        public virtual DbSet<tblGame> tblGames { get; set; }
        public virtual DbSet<tblMove> tblMoves { get; set; }
        public virtual DbSet<tblPit> tblPits { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public MancalaEntities(DbContextOptions<MancalaEntities> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public MancalaEntities()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define entity relationships and seed data
            CreatePlayers(modelBuilder);
            CreateGames(modelBuilder);
            CreatePits(modelBuilder);
            CreateMoves(modelBuilder);
            CreateUsers(modelBuilder);
        }



        private void CreateUsers(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < userId.Length; i++)
            {
                userId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUser_Id");

                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(28)
                    .IsUnicode(false);
                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[0],
                FirstName = "Steve",
                LastName = "Marin",
                UserId = "smarin",
                Password = GetHash("maple")
            });
            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[1],
                FirstName = "John",
                LastName = "Doro",
                UserId = "jdoro",
                Password = GetHash("maple")
            });
            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[2],
                FirstName = "Brian",
                LastName = "Foote",
                UserId = "bfoote",
                Password = GetHash("maple")
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[3],
                FirstName = "Other",
                LastName = "Other",
                UserId = "sophie",
                Password = GetHash("sophie")
            });
        }

        // Define relationships and seeding for Players
        private void CreatePlayers(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < playerId.Length; i++)
            {
                playerId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblPlayer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblPlayer_Id");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<tblPlayer>().HasData(
                new tblPlayer { Id = playerId[0], Username = "Taylor", Score = 0 },
                new tblPlayer { Id = playerId[1], Username = "Brittany", Score = 0 }
            );
        }
        private void CreatePits(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < pitId.Length; i++)
            {
                pitId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblPit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblPit_Id");
                entity.Property(e => e.Stones).IsRequired();
                entity.Property(e => e.IsMancala).IsRequired();
                entity.Property(e => e.PitPosition).IsRequired();
                entity.Property(e => e.PlayerId).IsRequired(); // Add this

                entity.HasOne(e => e.Game)
                      .WithMany(b => b.Pits)
                      .HasForeignKey(e => e.GameId)
                      .HasConstraintName("FK_tblGame_PitId");

                entity.HasOne(u => u.Player)
                      .WithMany(p => p.Pits)
                      .HasForeignKey(p => p.PlayerId)
                      .HasConstraintName("FK_tblPlayer_PitId");
            });

            List<tblPit> pits = new List<tblPit>
            {
                // Player 1 pits (positions 1-6)
                new tblPit { Id = pitId[0], GameId = gameId[0], PitPosition = 1, Stones = 4, PlayerId = playerId[0], IsMancala = false },
                new tblPit { Id = pitId[1], GameId = gameId[0], PitPosition = 2, Stones = 4, PlayerId = playerId[0], IsMancala = false },
                new tblPit { Id = pitId[2], GameId = gameId[0], PitPosition = 3, Stones = 4, PlayerId = playerId[0], IsMancala = false },
                new tblPit { Id = pitId[3], GameId = gameId[0], PitPosition = 4, Stones = 4, PlayerId = playerId[0], IsMancala = false },
                new tblPit { Id = pitId[4], GameId = gameId[0], PitPosition = 5, Stones = 4, PlayerId = playerId[0], IsMancala = false },
                new tblPit { Id = pitId[5], GameId = gameId[0], PitPosition = 6, Stones = 4, PlayerId = playerId[0], IsMancala = false },

                // Player 1 Mancala (position 7)
                new tblPit { Id = pitId[6], GameId = gameId[0], PitPosition = 7, Stones = 0, PlayerId = playerId[0], IsMancala = true },

                // Player 2 pits (positions 8-13)
                new tblPit { Id = pitId[7], GameId = gameId[0], PitPosition = 8, Stones = 4, PlayerId = playerId[1], IsMancala = false },
                new tblPit { Id = pitId[8], GameId = gameId[0], PitPosition = 9, Stones = 4, PlayerId = playerId[1], IsMancala = false },
                new tblPit { Id = pitId[9], GameId = gameId[0], PitPosition = 10, Stones = 4, PlayerId = playerId[1], IsMancala = false },
                new tblPit { Id = pitId[10], GameId = gameId[0], PitPosition = 11, Stones = 4, PlayerId = playerId[1], IsMancala = false },
                new tblPit { Id = pitId[11], GameId = gameId[0], PitPosition = 12, Stones = 4, PlayerId = playerId[1], IsMancala = false },
                new tblPit { Id = pitId[12], GameId = gameId[0], PitPosition = 13, Stones = 4, PlayerId = playerId[1], IsMancala = false },

                // Player 2 Mancala (position 14)
                new tblPit { Id = pitId[13], GameId = gameId[0], PitPosition = 14, Stones = 0, PlayerId = playerId[1], IsMancala = true }
            };

            modelBuilder.Entity<tblPit>().HasData(pits);
        }

        // Define relationships and seeding for Games
        private void CreateGames(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < gameId.Length; i++)
            {
                gameId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblGame_Id");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.CurrentTurn).IsRequired();
                entity.Property(e => e.DateCreated).HasColumnType("datetime");
                entity.Property(e => e.Player1Id).IsRequired();
                entity.Property(e=> e.Player2Id).IsRequired();
                entity.Property(e=> e.IsGameOver).IsRequired();
                entity.Property(e=>e.Status).IsRequired();

            });

            modelBuilder.Entity<tblGame>().HasData(
                new tblGame
                {
                    Id = gameId[0],
                    Player1Id = playerId[0],
                    Player2Id = playerId[1],
                    CurrentTurn = playerId[0],
                    IsGameOver = false,
                    Status = "Active",
                    DateCreated = DateTime.Now,
                }
            );
        
    }

        // Define relationships and seeding for Moves
        private void CreateMoves(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < moveId.Length; i++)
            {
                moveId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblMove>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblMove_Id");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.MoveNo).IsRequired();
                entity.Property(e => e.SourcePit).IsRequired();
                entity.Property(e => e.StonesMoved).IsRequired();
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                // Define the relationship between tblMove and tblGame
                entity.HasOne(m => m.Game)
                      .WithMany(g => g.Moves)
                      .HasForeignKey(m => m.GameId)
                      .HasConstraintName("FK_tblMove_GameId");

                // Define the relationship between tblMove and tblPlayer
                entity.HasOne(m => m.Player)
                      .WithMany(u => u.Moves)
                      .HasForeignKey(m => m.PlayerId)
                      .HasConstraintName("FK_tblMove_PlayerId");

                modelBuilder.Entity<tblMove>().HasData(
                    new tblMove
                    {
                        Id = moveId[0],
                        GameId = gameId[0],
                        PlayerId = playerId[0],
                        MoveNo = 1,
                        SourcePit = 2,
                        StonesMoved = 3,
                        TimeStamp = DateTime.Now,
                        IsExtraTurn = false
                    }
                );
            });
        }

        private static string GetHash(string Password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }
    }
}
