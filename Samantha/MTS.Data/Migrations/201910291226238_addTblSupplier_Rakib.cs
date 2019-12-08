namespace SCM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTblSupplier_Rakib : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "JL.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("JL.Supplier");
        }
    }
}
