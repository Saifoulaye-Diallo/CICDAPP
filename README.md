1. Développement de l'application Web :
.NET Core : Un framework puissant et flexible pour le développement d'applications web modernes.
 Il permet aux développeurs de créer des applications qui peuvent fonctionner sur plusieurs plateformes et systèmes d'exploitation.
Dans ce pipeline, .NET Core est utilisé pour construire l'application principale.

3. Tests fonctionnels :
Selenium WebDriver : Un outil d'automatisation de tests pour les applications web qui permet de simuler les interactions des utilisateurs dans un navigateur web.
Il est utilisé pour tester le comportement de l'application dans des conditions réelles.
NUnit : Un framework de tests pour .NET, utilisé pour définir et exécuter les tests unitaires et fonctionnels.
Il aide à valider que le code fonctionne comme prévu avant qu'il soit poussé dans le dépôt de code source.

5. Orchestration des déploiements :
Jenkins : Un serveur d'automatisation open source qui gère les cycles de développement des logiciels, de la construction à la livraison.
 Jenkins orchestre le processus de CI/CD en déclenchant et en gérant les tâches de build, test et déploiement en fonction des modifications apportées au code source.

4. Gestion de la configuration :
Puppet : Un outil de gestion de configuration qui permet aux administrateurs systèmes de déployer et de gérer automatiquement les serveurs et les logiciels.
Dans ce pipeline, Puppet assure que toutes les instances de l'application en environnement de test,
pré-production et production sont configurées de manière uniforme et selon les normes définies.

6. Hébergement de l'application :
IIS (Internet Information Services) : Un serveur web flexible pour héberger des applications Web .NET et autres. Dans ce pipeline, IIS sert à déployer et à héberger la version finale de l'application, garantissant sa disponibilité pour les utilisateurs et clients.
6. Gestion des versions et collaboration :

Git : Un système de contrôle de version distribué qui permet aux développeurs de suivre et gérer les modifications du code source au fil du temps. 
Il est essentiel pour permettre aux membres de l'équipe de collaborer sur le code sans conflits majeurs.
GitHub : Une plateforme de gestion de dépôts Git qui facilite la collaboration entre les développeurs. 
Sur GitHub, les équipes peuvent stocker leurs projets, gérer les versions, suivre les problèmes et contribuer à d'autres projets publics et privés. 
GitHub joue un rôle vital dans le pipeline de CI/CD en hébergeant le code source et en intégrant avec Jenkins pour automatiser les tests et les déploiements.


saifoulayediallo2019@gmail.com
