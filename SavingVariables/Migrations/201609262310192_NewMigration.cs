namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Variables", "VariableName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Variables", "VariableName");
        }
    }
}
