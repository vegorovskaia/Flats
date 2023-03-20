# Flats
To develop API

    1. Create an API for a client applications that display new buildings in St. Petersburg and the Leningrad region.
    2. Implement the API as a .NET Core Web API application without authorization.
    3. As a data source, use the MS Sql database. Table structure:
        3.1. Apartments
            3.1.1. Apartment ID
            3.1.2. House ID (foreign key to the house)
            3.1.3. Number of rooms
            3.1.4. Total area
            3.1.5. Floor
            3.1.6. Price
        3.2. Houses 
            3.2.1. House ID
            3.2.2. Stage number
            3.2.3. House number
            3.2.3. Living complex name
            3.2.4. District ID (foreign key to the district)
	3.3. Area
            3.3.1. District ID
            3.3.2. District name
            3.3.3. Region identifier (foreign key to the region)
        3.4. Region
            3.4.1. Region ID
            3.4.2. Region name
    4. Script for creating the database structure: create_tables.sql
    5. Script for inserting data into the database: insert_data.sql
    6. To interact with the database, use the Entity Framework.
    7. List of API endpoints:
        7.1. Method for getting a list of apartments
            7.1.1. List of apartment fields returned:
                7.1.1.1. Apartment ID
                7.1.1.2. Region name
                7.1.1.3. District name
                7.1.1.4. Living complex name
                7.1.1.5. Stage number
                7.1.1.6. House number
                7.1.1.7. Number of rooms
                7.1.1.8. Total area
                7.1.1.9. Floor
                7.1.1.10. Price
            7.1.2. Provide pagination
            7.1.3. Provide the ability to filter by:
                7.1.3.1. District
                7.1.3.2. Costs (from - to)
                7.1.3.3. House ID
            7.1.4. Provide sorting in ascending and descending order by:
                7.1.4.1. District
                7.1.4.2. Costs
        7.2. Method for deleting an apartment
        7.3. Method for editing an apartment (optional)

Upon completion of the task, provide:
    • source code

In SQL folder there are scripts for DB.