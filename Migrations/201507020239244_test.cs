namespace Edstart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parent", "SchoolId", "dbo.School");
            AddForeignKey("dbo.Parent", "SchoolId", "dbo.School", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parent", "SchoolId", "dbo.School");
            AddForeignKey("dbo.Parent", "SchoolId", "dbo.School", "ID", cascadeDelete: true);
        }
    }
}
