# Définition du nom de site
$site_name = 'gestionetudiants'

# Définition du chemin de base pour les dossiers
$base_path = "c:\\inetpub\\${site_name}"  

# Définition du chemin pour le dossier de production
$production_path = "${base_path}\\production"

# Définition du chemin pour le dossier de pré-production
$preproduction_path = "${base_path}\\preproduction"

# Définition des noms de pool d'applications
$pool_names = ['ProductionPool', 'PreProductionPool']

# Définition du port pour le site de production
$production_port = 5152

# Définition du port pour le site de pré-production
$preproduction_port = 5151

# Création des dossiers spécifiés s'ils n'existent pas déjà
file { [$base_path, $production_path, $preproduction_path]:
  ensure  => 'directory',
}

# Création des pools d'applications s'ils n'existent pas déjà
$pool_names.each |$app_pool| {
  exec { "create-${app_pool}-app-pool":
    command     => "C:\\Windows\\System32\\inetsrv\\appcmd.exe add apppool /name:${app_pool}",
    unless      => "C:\\Windows\\System32\\inetsrv\\appcmd.exe list apppool | findstr /i ${app_pool}",
    provider    => powershell,
    require     => File[$base_path],  # Assure que le répertoire existe avant de créer le pool
  }
}

# Configuration et démarrage des sites IIS
$site_configurations = {
  'ProductionSite' => {
    'site_path'        => $production_path,
    'app_pool'         => 'ProductionPool',
    'site_port'        => $production_port,
    'restart_required' => false,
  },
  'PreProductionSite' => {
    'site_path'        => $preproduction_path,
    'app_pool'         => 'PreProductionPool',
    'site_port'        => $preproduction_port,
    'restart_required' => true,
  },
}

# Parcours de chaque configuration de site
$site_configurations.each |$site_name, $config| {
  # Création du site IIS
  iis_site { $site_name:
    ensure          => 'started',                    # Assure que le site est démarré
    physicalpath    => $config['site_path'],         # Chemin physique du site
    applicationpool => $config['app_pool'],          # Pool d'applications utilisé par le site
    bindings        => [                             # Configuration des liaisons (bindings)
      {
        'protocol'           => 'http',               # Protocole HTTP
        'bindinginformation' => "*:${config['site_port']}:",
      },
    ],
    require         => Exec["create-${config['app_pool']}-app-pool"],  # Assure que le pool d'applications est créé avant le démarrage du site
  }

  # Redémarrage du site IIS si nécessaire
  if $config['restart_required'] {
    exec { "restart-${site_name}":
      command     => "C:\\Windows\\System32\\inetsrv\\appcmd.exe recycle apppool /apppool.name:${config['app_pool']}",
      refreshonly => true,  # La commande s'exécute uniquement si les dépendances sont modifiées
      subscribe   => Iis_site[$site_name],  # Cette ligne indique que l'exécution dépend du site spécifié
    }
  }
}

