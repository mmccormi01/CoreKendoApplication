# DDL For Pima County .Net Core Template

This is the instructions on how to create the schema and SQL tables for Microsoft Identity for the Pima County .Net Core template web app.

## Steps

1. Run the script **Schemas_Create.sql** to create the security schema used for the database tables.

2. Now the following scripts need to be ran in the following order to create the Identity tables needed by the template:

   * **AspNetUsers_Create.sql**
   * **AspNetRoles_Create.sql**
   * **AspNetUserRoles_Create.sql**
   * **AspNetUserTokens_Create.sql**
   * **AspNetUserClaims_Create.sql**
   * **AspNetRoleClaims_Create.sql**
   * **AspNetUserLogins_Create.sql**

3. In the Context.cs file in the PimaCountyTemplateDevCoreWeb project change the modelBuilder.HasDefaultSchema("IDENTITYSCHEMA"); to modelBuilder.HasDefaultSchema("security"); 

