using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MSOFT.Entities.Models;

namespace MSOFT.DL
{
    public partial class bida69Context : DbContext
    {
        public static string ConnectionString = string.Empty;
        public bida69Context()
        {
            Database.OpenConnection();
        }

        public bida69Context(DbContextOptions<bida69Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<InventoryCategory> InventoryCategory { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Ref> Ref { get; set; }
        public virtual DbSet<RefDetail> RefDetail { get; set; }
        public virtual DbSet<RefService> RefService { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<ViewRef> ViewRef { get; set; }
        public virtual DbSet<ViewRefDetail> ViewRefDetail { get; set; }
        public virtual DbSet<ViewRefService> ViewRefService { get; set; }
        public virtual DbSet<ViewServiceInfo> ViewServiceInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_dbo.AspNetUserRoles");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasComment("Khách hàng");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerGroupId).HasColumnName("CustomerGroupID");

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.IndentityCard).HasMaxLength(255);

                entity.Property(e => e.LevelId).HasColumnName("LevelID");

                entity.Property(e => e.Mobile).HasMaxLength(25);

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasComment("Nhân viên");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.EmployeeName).HasMaxLength(255);

                entity.Property(e => e.Mobile).HasMaxLength(25);

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasComment("Danh mục hàng hóa");

                entity.Property(e => e.InventoryId)
                    .HasColumnName("InventoryID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasComment("Mô tả");

                entity.Property(e => e.Inactive).HasComment("Ngừng kinh doanh (1: ngừng kinh doanh, 0: Đang kinh doanh)");

                entity.Property(e => e.InventoryCategoryId)
                    .HasColumnName("InventoryCategoryID")
                    .HasComment("Nhóm hàng hóa");

                entity.Property(e => e.InventoryCode)
                    .HasMaxLength(25)
                    .HasComment("Mã hàng");

                entity.Property(e => e.InventoryName)
                    .HasMaxLength(255)
                    .HasComment("Tên hàng hóa");

                entity.Property(e => e.InventoryType).HasComment("Loại hàng hóa");

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Bản ghi ở trạng thái bị xóa (0: chưa xóa, 1: đã xóa)");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(255)
                    .HasComment("Người thực hiện chỉnh sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày chỉnh sửa gần nhất");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasComment("Đơn giá (Giá mua hiện tại)");

                entity.Property(e => e.Quantity).HasComment("Tổng số lượng");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasComment("Đơn vị tính");

                entity.HasOne(d => d.InventoryCategory)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.InventoryCategoryId)
                    .HasConstraintName("FK_Service_ServiceType");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Inventory_Unit");
            });

            modelBuilder.Entity<InventoryCategory>(entity =>
            {
                entity.Property(e => e.InventoryCategoryId).HasColumnName("InventoryCategoryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryCategoryName).HasMaxLength(255);

                entity.Property(e => e.ModifiedBy).HasMaxLength(255);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Ref>(entity =>
            {
                entity.Property(e => e.RefId)
                    .HasColumnName("RefID")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Khoas chinhs");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasComment("Khách hàng");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .HasComment("Đối tượng xuất");

                entity.Property(e => e.JournalMemo)
                    .HasMaxLength(255)
                    .HasComment("Diễn giải");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người thực hiện chỉnh sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày chỉnh sửa gần nhất");

                entity.Property(e => e.RefDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày lập phiếu");

                entity.Property(e => e.RefNo)
                    .HasMaxLength(20)
                    .HasComment("Số phiếu");

                entity.Property(e => e.RefState)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Trạng thái hóa đơn(0: chưa thanh toá ; 1: đã thanh toán; 2: Hóa đơn nợ; 3: Hóa đơn bị hủy;)");

                entity.Property(e => e.RefType).HasComment("Loại phiếu");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasComment("Tổng tiền (nếu tổng tiền null là chưa thực hiện thanh toán) - đây là tổng tiền thanh toán của hóa đơn nên có thể khác tổng tiền dịch vụ + tổng tiền hàng hóa.");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Ref)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Ref_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Ref)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Ref_Employee");
            });

            modelBuilder.Entity<RefDetail>(entity =>
            {
                entity.HasComment("Chi tiết chứng từ");

                entity.Property(e => e.RefDetailId)
                    .HasColumnName("RefDetailID")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Khóa chính");

                entity.Property(e => e.CostPrice)
                    .HasColumnType("money")
                    .HasComment("Giá trị nhập vào");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("datetime")
                    .HasComment("Hạn sử dụng");

                entity.Property(e => e.InventoryId)
                    .HasColumnName("InventoryID")
                    .HasComment("Khóa ngoại");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người thực hiện chỉnh sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày chỉnh sửa gần nhất");

                entity.Property(e => e.Quantity)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Số lượng");

                entity.Property(e => e.RefId)
                    .HasColumnName("RefID")
                    .HasComment("Khóa ngoại");

                entity.Property(e => e.StockId)
                    .HasColumnName("StockID")
                    .HasComment("Kho");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Đơn giá (Giá bán)");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.RefDetail)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefDetail_Service");

                entity.HasOne(d => d.Ref)
                    .WithMany(p => p.RefDetail)
                    .HasForeignKey(d => d.RefId)
                    .HasConstraintName("FK_RefDetail_Ref");
            });

            modelBuilder.Entity<RefService>(entity =>
            {
                entity.Property(e => e.RefServiceId)
                    .HasColumnName("RefServiceID")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Khóa chính");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian kết thúc");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người thực hiện chỉnh sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày chỉnh sửa gần nhất");

                entity.Property(e => e.RefId).HasColumnName("RefID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian bắt đâu");

                entity.Property(e => e.UniPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Đơn giá");

                entity.HasOne(d => d.Ref)
                    .WithMany(p => p.RefService)
                    .HasForeignKey(d => d.RefId)
                    .HasConstraintName("FK_RefService_Ref");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.RefService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_RefService_Service");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Khóa chính");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasComment("Mô tả");

                entity.Property(e => e.InUse).HasComment("Đang sử dụng(1: đang sử dụng)");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người thực hiện chỉnh sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày chỉnh sửa gần nhất");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Giá dịch vụ");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("Tên dịch vụ");

                entity.Property(e => e.ServiceType)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Loại dịch vụ (0: Bida, 1: Khác)");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian bắt đầu sử dụng dịch vụ");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasComment("Đơn vị tính");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người thực hiện chỉnh sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày chỉnh sửa gần nhất");

                entity.Property(e => e.UnitName).HasMaxLength(255);
            });

            modelBuilder.Entity<ViewRef>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Ref");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.JournalMemo).HasMaxLength(255);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RefDate).HasColumnType("datetime");

                entity.Property(e => e.RefId).HasColumnName("RefID");

                entity.Property(e => e.RefNo).HasMaxLength(20);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TotalAmountInventory).HasColumnType("money");

                entity.Property(e => e.TotalAmountService).HasColumnType("decimal(38, 8)");
            });

            modelBuilder.Entity<ViewRefDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_RefDetail");

                entity.Property(e => e.CostPrice).HasColumnType("money");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.InventoryName).HasMaxLength(255);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RefDetailId).HasColumnName("RefDetailID");

                entity.Property(e => e.RefId).HasColumnName("RefID");

                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.Property(e => e.UnitName).HasMaxLength(255);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<ViewRefService>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_RefService");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.EndTimeToPay).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RefId).HasColumnName("RefID");

                entity.Property(e => e.RefServiceId).HasColumnName("RefServiceID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TotalTime).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<ViewServiceInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_ServiceInfo");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RefId).HasColumnName("RefID");

                entity.Property(e => e.RefServiceId).HasColumnName("RefServiceID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TotalAmountInventory).HasColumnType("money");

                entity.Property(e => e.UniPrice).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
