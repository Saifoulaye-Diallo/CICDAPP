pipeline {
    // Déclare un pipeline et spécifie que ce pipeline peut s'exécuter sur n'importe quel agent disponible.
    agent any

    // Définir des variables d'environnement utilisées dans tout le pipeline.
    environment {
        PRODUCTION_DIR = 'C:\\inetpub\\gestionetudiants\\production\\'  // Chemin du répertoire de production
        PREPRODUCTION_DIR = 'C:\\inetpub\\gestionetudiants\\preproduction\\'  // Chemin du répertoire de préproduction
        DOTNET = "C:\\Program Files\\dotnet\\dotnet.exe"
        PUPPET = "C:\\Program Files\\Puppet Labs\\Puppet\\bin\\puppet.bat"  // Chemin de l'exécutable Puppet
    }
        
    // Définition des différentes étapes du pipeline
    stages {
       
        // Étape de récupération du code source
        stage('Checkout') {
            steps {
                // Clone le répertoire git spécifié dans la branche 'master' en utilisant les identifiants donnés
                git credentialsId: '6672a1f2-2b4c-46a7-a379-bb4984ee7d06', url: 'https://github.com/Saifoulaye-Diallo/CICD-.NET.git', branch: 'master'
            }
        }
        // Étape de restauration des packages NuGet
        stage('Restore') {
            steps {
                // Restaure les dépendances nécessaires pour le projet
              bat "\"${env.DOTNET}\" restore \"${WORKSPACE}\\SeleniumApp.sln\""
            }
        }
        // Étape de construction de l'application
        stage('Build') {
            steps {
                // Construit le projet en mode Release sans restaurer les packages (car déjà fait)
                bat "\"${env.DOTNET}\" build \"${WORKSPACE}\\SeleniumApp.sln\" --configuration Release --no-restore"
            }
        }
         // Étape de configuration initiale avec Puppet
        stage('Puppet Configuration') {
            steps {
                script {
                    // Exécute un script Puppet pour appliquer un manifeste, préparant l'environnement
                    bat "\"${env.PUPPET}\" apply \"${WORKSPACE}\\init.pp\""
                }
            }
        }
        // Étape de publication dans l'environnement de préproduction
        stage('Publish-Preproduction') {
            steps {
                // Arrête le service IIS pour permettre la publication
                bat 'net stop "w3svc" /y'
                // Publie l'application dans le répertoire de préproduction spécifié
                  bat "\"${env.DOTNET}\" publish \"${WORKSPACE}\\SeleniumApp\\SeleniumApp.csproj\" --configuration Release --output \"${env.PREPRODUCTION_DIR}\" --no-restore --no-build"
                // Redémarre le service IIS après la publication
                pipeline {
    // Déclare un pipeline et spécifie que ce pipeline peut s'exécuter sur n'importe quel agent disponible.
    agent any

    // Définir des variables d'environnement utilisées dans tout le pipeline.
    environment {
        PRODUCTION_DIR = 'C:\\inetpub\\gestionetudiants\\production\\'  // Chemin du répertoire de production
        PREPRODUCTION_DIR = 'C:\\inetpub\\gestionetudiants\\preproduction\\'  // Chemin du répertoire de préproduction
        DOTNET = "C:\\Program Files\\dotnet\\dotnet.exe"
        PUPPET = "C:\\Program Files\\Puppet Labs\\Puppet\\bin\\puppet.bat"  // Chemin de l'exécutable Puppet
    }
        
    // Définition des différentes étapes du pipeline
    stages {
       
        // Étape de récupération du code source
        stage('Checkout') {
            steps {
                // Clone le répertoire git spécifié dans la branche 'master' en utilisant les identifiants donnés
                git credentialsId: '6672a1f2-2b4c-46a7-a379-bb4984ee7d06', url: 'https://github.com/Saifoulaye-Diallo/CICD-.NET.git', branch: 'master'
            }
        }
        // Étape de restauration des packages NuGet
        stage('Restore') {
            steps {
                // Restaure les dépendances nécessaires pour le projet
              bat "\"${env.DOTNET}\" restore \"${WORKSPACE}\\SeleniumApp.sln\""
            }
        }
        // Étape de construction de l'application
        stage('Build') {
            steps {
                // Construit le projet en mode Release sans restaurer les packages (car déjà fait)
                bat "\"${env.DOTNET}\" build \"${WORKSPACE}\\SeleniumApp.sln\" --configuration Release --no-restore"
            }
        }
         // Étape de configuration initiale avec Puppet
        stage('Puppet Configuration') {
            steps {
                script {
                    // Exécute un script Puppet pour appliquer un manifeste, préparant l'environnement
                    bat "\"${env.PUPPET}\" apply \"${WORKSPACE}\\init.pp\""
                }
            }
        }
        // Étape de publication dans l'environnement de préproduction
        stage('Publish-Preproduction') {
            steps {
                // Arrête le service IIS pour permettre la publication
                bat 'net stop "w3svc" /y'
                // Publie l'application dans le répertoire de préproduction spécifié
                  bat "\"${env.DOTNET}\" publish \"${WORKSPACE}\\SeleniumApp\\SeleniumApp.csproj\" --configuration Release --output \"${env.PREPRODUCTION_DIR}\" --no-restore --no-build"
                // Redémarre le service IIS après la publication
                bat 'net start "w3svc"'
            }
        }
        // Étape de test de l'application en préproduction
        stage('Test Preproduction') {
            steps {
                // Exécute les tests unitaires du projet spécifié
            bat "\"${env.DOTNET}\" test \"${WORKSPACE}\\SeleniumTest\\SeleniumTest.csproj\" --no-restore --verbosity normal"
            }
        }
        // Étape de publication en production
        stage('Publish-production') {
            when {
                // Condition pour continuer cette étape seulement si les étapes précédentes ont réussi
                expression {
                    currentBuild.result == null || currentBuild.result == 'SUCCESS'
                }
            }
            steps {
                // Arrête le service IIS avant la publication
               //bat 'net stop "w3svc" /y'
                // Publie l'application dans le répertoire de production spécifié
                bat "\"${env.DOTNET}\" publish \"${WORKSPACE}\\SeleniumApp\\SeleniumApp.csproj\" --configuration Release --output \"${env.PRODUCTION_DIR}\" --no-restore --no-build"
                // Redémarre le service IIS après la publication
                bat 'net start "w3svc"'
            }
        }
    }

    // Actions à effectuer après l'exécution du pipeline
    post {
        success {
            // Message en cas de succès du pipeline
            echo 'La construction et la publication ont réussi.'
        }
        failure {
            // Message en cas d'échec du pipeline
            echo 'La construction ou la publication a échoué.'
        }
    }
}
            }
        }
        // Étape de test de l'application en préproduction
        stage('Test Preproduction') {
            steps {
                // Exécute les tests unitaires du projet spécifié
            bat "\"${env.DOTNET}\" test \"${WORKSPACE}\\SeleniumTest\\SeleniumTest.csproj\" --no-restore --verbosity normal"
            }
        }
        // Étape de publication en production
        stage('Publish-production') {
            when {
                // Condition pour continuer cette étape seulement si les étapes précédentes ont réussi
                expression {
                    currentBuild.result == null || currentBuild.result == 'SUCCESS'
                }
            }
            steps {
                // Arrête le service IIS avant la publication
                bat 'net stop "w3svc" /y'
                // Publie l'application dans le répertoire de production spécifié
                bat "\"${env.DOTNET}\" publish \"${WORKSPACE}\\SeleniumApp\\SeleniumApp.csproj\" --configuration Release --output \"${env.PRODUCTION_DIR}\" --no-restore --no-build"
                // Redémarre le service IIS après la publication
                bat 'net start "w3svc"'
            }
        }
    }

    // Actions à effectuer après l'exécution du pipeline
    post {
        success {
            // Message en cas de succès du pipeline
            echo 'La construction et la publication ont réussi.'
        }
        failure {
            // Message en cas d'échec du pipeline
            echo 'La construction ou la publication a échoué.'
        }
    }
}
