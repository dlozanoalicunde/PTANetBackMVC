param(
	[String]$migrationName = $null
)

$context = "AlicundeTest.Persistence.AlicundeTestDbContext"
$project = "../AlicundeTest.Persistence/"
$migrationsPath = "./Migrations"


if ($?) {
	dotnet build --no-restore
	"Generating migration:"
	dotnet ef migrations add $migrationName -c $context -o $migrationsPath -p $project
		
}