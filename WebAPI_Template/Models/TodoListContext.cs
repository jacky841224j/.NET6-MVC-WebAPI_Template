using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_Template.Models;

public partial class TodoListContext : DbContext
{
    public TodoListContext()
    {
    }

    public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<TodoList> TodoLists { get; set; }

    public virtual DbSet<UploadFile> UploadFiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TodoList;Integrated Security=True ; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId);

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId)
                .HasMaxLength(5)
                .HasColumnName("EMP_ID");
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Position)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_List");

            entity.ToTable("TodoList");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Enable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Event).HasMaxLength(50);
            entity.Property(e => e.InsertEmployeeId)
                .HasMaxLength(10)
                .HasColumnName("InsertEmployeeID");
            entity.Property(e => e.InsertTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateEmployeeId)
                .HasMaxLength(10)
                .HasColumnName("UpdateEmployeeID");
            entity.Property(e => e.UpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<UploadFile>(entity =>
        {
            entity.ToTable("UploadFile");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmpId)
                .HasMaxLength(5)
                .HasColumnName("EMP_ID");
            entity.Property(e => e.FileName).HasMaxLength(50);
            entity.Property(e => e.ListId).HasColumnName("ListID");
            entity.Property(e => e.Src).HasMaxLength(50);

            entity.HasOne(d => d.Emp).WithMany(p => p.UploadFiles)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_UploadFile_UploadFile");

            entity.HasOne(d => d.List).WithMany(p => p.UploadFiles)
                .HasForeignKey(d => d.ListId)
                .HasConstraintName("FK_UploadFile_TodoList");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
