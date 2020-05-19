
## Introduction

YAARTS: Yet Again Another Real Time Strategy (Game). Est un prototype de jeu de stratégie en temps réel (Dune 2, Command & Conqueres, Warcraft, Starcraft, ...) réalisé conjointement pour les projets de fin d'année des cours d'infographie et de .NET.

Le terme *jeu de stratégie en temps réel* <st c="r">NOTE RTS À PARTIR DE MTNT</st> est utilisé pour la première fois en 1992 <st c="r">SOURCE: https://arstechnica.com/gaming/2017/09/build-gather-brawl-repeat-the-history-of-real-time-strategy-games/</st> pour désigner le genre du jeu Dune II basé sur le roman éponyme de Frank Herbert. Depuis, le genre à beaucoup évolué mais principalement graphiquement. En effet, bien que la définition précise fasse l'objet de débats, les RTS sont traditionnellement définis par les verbes "**récolter**", "**construire**" et "**détruire**" en plus d'être des jeux où l'action se déroule en temps réel entre les différents participants <st c="r">SOURCE: https://arstechnica.com/gaming/2017/09/build-gather-brawl-repeat-the-history-of-real-time-strategy-games/</st>. En partant de ces trois verbes, il est facilement déductible que les intentions des joueurs sont axés autour du fait de "gérer des ressources", "développer une base" et "créer des unités" pour combattre l'adversaire. Ces trois critères sont donc naturellement les objectifs du design de YAARTS.

## Contextualisation

Ce projet est en essence inspiré par ce qui a été fait au semestre de printemps 2019 pour le projet P2 Java: YARTS (avec un seul "A", liens en annexe). Ce projet fut une premier tentative d'implémentation d'un RTS. Sans aucune expérience initiale avec la technologie utilisée <st c="r">NOTE: LIBGDX</st> et encore moins avec les techniques d'infographie et fût l'occasion de cerner le problème et d'avoir une idée clair de quel sont les principaux obstacles et pièges à éviter. 

YAARTS (avec deux "A") malgré sa dimension supplémentaire a pour but d'aller plus loin que le premier et de proposer un prototype de jeu de stratégie qui implémente toutes les fonctionnalités qui définissent le genre <st c="r">NOTE: Décrites dans l'introduction</st> le tout dans un style visuel agréable et cohérent.

### Cours de .NET

Un jeu vidéo est un sport complet qui demande de nombreuses qualités sur lesquelles le cours de .NET a mit l'accent. En effet, dans un jeu vidéo, de nombreux composants hétéroclites coexistent et communiquent. Dans ces circonstances et à cette échelle, de nombreux problèmes qui dans le cadre de projets moins vastes n'ont pas le temps de surgir, ont ici tout le loisir de se dévoiler et de bloquer la progression. Pour palier à ça, il est impératif d'observer une certaine rigueur de bout en bout et c'est bien là un des points clé qui est revenu dans tous les travaux du cours de .NET.

Au vue des contraintes temporelles et de l'ambition dans les objectifs, il est nécessaire d'avoir une idée clair de ce que doit faire l'application ainsi que du niveau de finition souhaité. Le premier point permet de réfléchir à une architecture cohérente et le second à définit à partir de quel niveau il est possible de travailler sans plan car les conséquences de code de moins bonne qualité aura peu ou pas d'impacte n'ayant pas vocation à être étendu. Cette façon de faire permet d'avoir une base solide et robuste qui peut soutenir du code plus volatile et expérimental. De plus, cette façon de faire est cohérente dans le contexte d'un projet à cheval sur deux matières dont l'une qui est en pleine phase de découverte au niveau de ses concepts.

Le but de la partie C# de ce projet est donc de créer un moteur de jeu de stratégie en temps réel suffisamment robuste pour qu'il puisse servir de terrain d'expérimentation pour la partie infographie du projet.

### Cours d'infographie

Le projet décrit dans ce rapport étant un jeu vidéo, les liens avec le cours d'infographie sont nombreux. De façon générale, les notions du premier semestre sont la base de culture générale nécessaire à pouvoir naviguer sereinement dans les problèmes plus haut niveau que pose Unity. La matière du second semestre quant à elle est l'aperçu nécessaire pour pouvoir entamer un projet plus conséquent que quelques scripts qui se courent après.

Même si la partie purement technique du pipeline OpenGL n'a pas été directement utilisée, les notions liées aux *shaders* et leurs déclinaisons *vertex* et *fragments* sont au coeur du développement de jeux vidéos. Malgré leur caractère bas niveau, ces notions reviennent dans de nombreuses discussions qui touchent des aspects plus haut niveau et dont il ne serait pas possible de saisir la pertinence ou les nuances sans avoir une idée du contexte dans lequel elles s'inscrivent.

Les notions de scène et de caméra sont fondamentales et reviennent systématiquement. Le fait de bien comprendre la notion de repère est un gain cognitif indispensable pour comprendre de nombreuses explications où les conversions sont faites implicitement en assumant que le lecteur jongle avec ces notions. De la même façon, une bonne compréhension des projection et de leur spécificités ainsi que la capacité de visuellement les différencier sont autant de place mentale libérée pour pouvoir résoudre d'autres problèmes qui assument que ces notions sont acquises.

Au travers du projet dont fait état le présent rapport, une bonne partie des notions du premier semestre ont été touchées mais au travers du prisme d'Unity qui apporte une vision considérablement plus haut niveau. Cette perspective nouvelle apporte des explications sous la forme de complément là ou les questions n'était pas forcément évidentes à formuler dans le contexte bas niveau du premier semestre. Typiquement, de part leur simplicité à mettre en place dans Unity, les différents types d'éclairages, leurs attributs et leur relation avec la physique permet de gagner un niveau d'abstraction qui automatiquement fait assimiler un certain nombre de concepts plus basiques. De même, les outils que propose Unity pour visualiser et jouer avec les matériaux, les modèles et les textures permet d'encrer les notions d'UV <st c="r">NOTE: Coordonnées de textures dans le cours</st>, de normales, les déformations causées par les application de textures et de leur comportement vis-à-vis de la lumière.

Autrement dit, de façon plus pragmatique, le cours d'infographie donne une vision suffisamment globale de la matière pour permettre entre autres de ne pas perdre de temps à lire un article sur la gestion des ombres dans l'herbe via programmation de shader alors que ce qui est recherché est le type d'éclairage qui serait le plus pertinent pour une scène.

## Analyse

Le périmètre d'un jeu vidéo étant très vaste et les développement prenant régulièrement des années pour des équipes professionnelles, il est nécessaire d'être réaliste dans les objectifs visés pour qu'ils restent cohérent avec le contexte académique du projet. Ainsi, ce chapitre a pour bût d'expliquer la démarche suivie pour définir le projet ainsi que ses objectifs abstraits.

### Prototype utilisable

Le principal objectif du projet est de fournir un *prototype utilisable*. Il est donc avant tout nécessaire de s'entendre sur ce que veut dire ce terme. Ainsi, dans le présent contexte, un prototype utilisable est une application interactive qui permet au testeur, guidé par les développeurs de se faire une idée de ce à quoi pourrait ressembler le produit dans un niveau de finition plus complet. Les limitations sont donc nombreuses et l'application ne fonctionne que dans un cadre restreint. 

Autrement dit, le jeu n'est pas jouable au sens *fun* du terme mais il est testable et peut être le support d'une démonstration.

De plus, il est important de signaler que l'implémentation tourne autour des mécaniques essentielles et non pas autour des règles du jeu. Cela veut dire que le jeu n'est aucunement balancé et n'a pas de bût ou de conditions de victoire.

Finalement, son style graphique et son "contexte historique" sont uniquement motivés par leur accessibilité et n'ont pas été le fruit d'une réflexion particulière.

### Objectifs


Le bût est donc de créer un prototype utilisable de RTS et pour ce faire, d'amblé sera écarté le gros de la notion d'adversaire qui sera réduite au stricte minimum. Les objectifs sont donc essentiellement axés autour des trois verbes "**récolter**", "**construire**" et "**détruire**", introduits dans les première lignes de ce rapport.

À ces trois verbes viennent s'ajouter les trois notions suivantes:
* **Actions semi autonome** : Cela implique que les entités du jeu sont capable d'une certaine autonomie. En effet, une fois un ordre donné par le joueur, l'entité doit être capable d'exécuter cet ordre en prenant en charge de gérer la diversité des obstacles qui le séparent de l'accomplissement de l'ordre. Pour ce faire, les entités doivent faire preuve d'une certaine intélligence, d'une capacité d'adaptation et doivent savoir quoi faire en cas d'impossibilité d'exécuter l'ordre. 
* **Temps réel** : Par opposition à tour par tour, la notion de temps réel implique que le temps passe de façon continue et n'est pas échantillonné et partagé entre les joueurs comme pour un jeu de plateau ou un jeu de cartes traditionnel. Peu importe ce que fait le joueur, certaines actions se déroulent en arrière plan "en même temps". Cela implique qu'il est nécessaire d'avoir des mécanismes qui assurent que ces actions seront menées à bien sans forcément demander d'intervention implicite du joueur.
* **Exploration** : Ou autrement dit, le fait de dissimuler l'objet de l'exploration et implique donc la nécessité de la mise en place de mécanismes qui permettent de cacher des informations au joueur pour que ce dernier puisse les découvrir en exécutant certaines actions.

#### Prototype utilisable

Il est important de définir ce qui est entendu par "prototype utilisable". Dans le présent contexte, un prototype utilisable est une application interactive qui permet au testeur, guidé par les développeurs de se faire une idée de ce à quoi pourrait ressembler le produit dans un niveau de finition plus complet. Les limitations sont donc nombreuses et l'application ne fonctionne que dans un cadre restreint. 

Autrement dit, le jeu n'est pas jouable au sens *fun* du terme mais il est testable et peut être le support d'une démonstration.

De plus, il est important de signaler que l'implémentation tourne autour des mécaniques essentielles et non pas autour des règles du jeu. Celà veut dire que le jeu n'est aucunement balancé et n'a pas de bût ou de conditions de victoire. Son style graphique et son "contexte historique" sont uniquement motivés par leur accessibilité et n'ont pas été le fruit d'une réflexion particulière.

#### MVP

Dans ce point sont détaillés les objectifs de façon grossière mais spécifique pour atteindre les objectifs des deux points précédents:

* Une carte
* Une faction
* Entités:
    * Offensives (mêlée, distance)
    * Bâtiments (production)
    * Récolte (ressources)
* IA des entités:
    * Pathfinding
    * Collision avoidance (gestion des obstacles mobiles dans le pathfinding)
    * Système d'états
    * Gestion de la ligne de vue et de la portée
    * Gestion des actions
    * Gestion des interactions
    * Gestion de la durabilité
    * Système d'appartenance (à un joueur ou à l'ordinateur)
* Mécanisme de sélection des entités (simple et multiple)
* Gestion des ressources
* Gestion de la caméra
* Brouillard de guère
* GUI
    * Une mini carte
    * Affichage des ressources
    * Affichage de la population
    * Panneau de sélection
    * Panneau des actions de la sélection
    * Curseur intelligent dont la forme dépend du contexte

#### Utopique

* IA qui joue l'adversaire
* Diversifications des cartes de jeu
* Génération procédurale de cartes
* Éléments de gameplay (technologies, amélioration des unités, variété dans les unités)
* GUI moins prototype
* Tout le reste

### Répartition des tâches

Cette partie décrit la répartition des tâches au sein du projet.

#### Sol Rosca (Architecture)
* Architecture générale
* Architecture des entités
* Mécanisme de sélection
* IA des entités
* Unités offensives
* Gestion des ressources
* Unité de prod/récolte
* Bâtiment de production
* Communication moteur-gui

#### Nathan Latino (GUI)
* Menu principal
* Mini-carte
* Affichage des ressources
* Affichage de la population
* Panneau de sélection
* Panneau d'action de la sélection

#### Tristan Seuret (Features)
* Étude des solutions de versioning
* Étude des pipelines graphiques d'Unity (Standard et URP)
* Fog Of War
* Curseur intelligent dont la forme dépend du contexte si possible

<st c="rgb">CONTAINER: INFO</st>
Concernant la rédaction du rapport chaque membre s'est occupé des parties concernant son travail et Sol Rosca a pris en charge les parties communes.


### Travail collaboratif

Après quelques recherches, il s'est avéré que l'utilisation d'un git tel que Github n'est pas adapté à Unity. En effet, Unity est un programme très graphique et l'utilisation d'un logiciel de versioning pour des fichiers textes n'est pas adaptée. De plus, Unity générant un nombre important de fichiers, les conflits de _merge_ sont plus délicat à gérer qu'à l'accoutumée. Il existe néanmoins des méthodes et des configurations permettant de travailler collaborativement sur un projet Unity. Cependant, suite à ces recherches, Unity Collaborate fût découvert et s'est vite imposé comme une solution évidente. 

Unity, conscient de la nécessité d'offrir un moyen de collaborer sur le même projet, a développé un service similaire à Git permettant la synchronisation et le versioning d'un projet. Ce dernier se nomme Unity Collaborate et est inclut dans le [Github Student Developper Pack](https://education.github.com/pack) depuis le mois de Janvier 2020. Ce dernier offre plusieurs avantages notables par rapport à Git : 

* Facilité d'initialisation
L'utilisation d'un git aurait demandé la configuration de ce dernier, la configuration de Unity ainsi que la configuration de Git LFS (**L**arge **F**ile **S**ystem). De plus, il est directement intégré au sein de Unity et permet de facilement récupéré les derniers changements.

* Cloud build
Lors de l'utilisation de Unity Teams (qui contient Unity Collaborate), il est possible de build le projet directement sur les serveurs Unity.
![Cloud build interface](https://i.ibb.co/fCc6xMP/image.png)
Comme il est possible de le remarquer sur cette image, une build demande en moyenne une vingtaine de minutes pour être créée. Non seulement les créées sur les serveurs Unity permet de s'affranchir d'un poids, il permet également de facilement distribué les builds à l'entièreté de l'équipe et d'en garder une traçabilité.

* Modification de fichiers
Comme expliqué précédemment, la modification concurrente d'une scène pose d'énormes problèmes de conflits lors de _merge_. Unity Collaborate, totalement intégré à Unity, permet d'afficher qu'un fichier est en cours de modifications par une autre membre de l'équipe et permet d'éviter ce genre de conflit. 
![informations modification en cours d'un fichier](https://i.ibb.co/kQbYvmc/image.png)

* Centralisation des fonctionnalités
![Dashboard Unity](https://i.ibb.co/HNzD8c4/image.png)
De plus, l'utilisation de Unity Collaborate permet d'avoir un point central pour toutes les informations du projet. Que ce soit les retours utilisateurs, le versioning ou encore les builds, tout est accessible à un seul endroit.

Cependant, Unity Collaborate ne propose aucun système de branches comme il est disponible dans les autres logiciels de versioning.

De ce fait, afin d'éviter les conflits lors de merge ou autre désagrément, lorsqu'une personne souhaitait modifier la scène et tester sa solution, il faisait une copie de la scène localement. Ensuite, une fois la fonctionnalité aboutit, elle était réimplémenter dans la scène final en s'assurant que personne d'autre ne fasse de modification sur la scène.

## Conception

Ce chapitre couvre la concrétisation des abstractions du précédent chapitre. Il énonce quelles sont les spécifications techniques du projet ainsi que les moyens techniques mis en œuvre pour les réaliser.

### Spécifications

Dans cette partie seront développé et mis en termes techniques les objectifs du précédent chapitre en commençant par une fiche technique générale pour caractériser plus précisément le projet.

<st c="r">NOTE: La consigne d'un rapport détaillé étant arrivé tard, certains points n'ont que peu été discutés et vu la charge de travaille, de nombreuses choses ont étés implémentées en testant directement dans le moteur. Ces éléments n'ont pas été produits en suivant des spécifications. 

* Logique d'affichage du rectangle de sélection
* Affichage de la sélection (cercle à ses pieds) 
* Barres de vie
* Système de gestion des meshs
* Système de gestion des particules
* Gestion des collision
* Pathfinding

Cette partie décrit uniquement ce qu'il semblait capital de définir tôt dans le projet

</st>

#### Général

* **Plateforme**: PC
* **Os**: Windows & Linux
* **Langage**: C#
* **Vue**: 3D
* **Moteur de rendu**: Unity3D
* **Camera**: Perspective
* **Contrôle**: Clavier + souris
* **Genre**: Stratégie
* **Sous-genre**: Temps réel
* **Type de joueurs**: 
    * Humain (`Player`)
    * Ordinateur (`CPU`)
* **Contexte scénaristique**: 
  * Médiéval 
  * Toon
* **Économie**: 
    * Récolte de ressources

#### Entités

Les entités sont de trois types différents qui remplissent chacun un rôle spécifique:
* Character
* Structure
* Collectible 

Ces trois types ont en commun quatre caractéristiques principales : 
* Le fait d'avoir un **propriétaire**
* Le fait d'être **sélectionnable**  
* Le fait de posséder un certain nombre de **points de "quelque chose"** (vie, durabilité, ressource, ...) qui quantifie une durée de vie 
* Le fait de possséder **un état**. 

Une entité appartient donc à un joueur, à l'ordinateur ou à la carte (gaïa) et une fois ses points épuisés, elle change d'état et passe dans un état où elle n'est plus sélectionnable (et par extension, n'est plus une entité).

><st c="r">CONTAINER INFO: </st> P.ex. les éléments de décor ou les projectiles ne sont pas des entités car elles ne possède pas ces 4 caractéristiques. Ce sont néanmoins des objets du jeu qui possède des attributs et servent des fonctions, ce ne sont juste pas des entités. 

C'est les caractéristiques particulières d'une entité qui définissent son **type**. 
Aussi, un type peut posséder des **déclinaisons** dans le cas où sa façon d'interagir avec le monde, ses états ou sa façon de gérer ses points n'est pas générique. Finalement, la dernière variation au sein d'un type est appelée **classe** et ne diffère des autres membres de son type que par les valeurs de base de ses attributs logiques et/ou esthétiques. **Dans ce projet, une classe est la concrétisation d'un type**.

##### Type Character

Un Character:
* **Est mobile** : 
  * Pathfinding
  * Peut être nulle pour un certain temps
  * Mémoire
* **Est conscient du monde qui l'entour** : 
  * Zone de perception (Ligne de vue)
  * Gestion des collisions (directes & perception),
  * Mémoire
* **Est capable d'interagir**:
  * Avec le sol (un point vide de la carte) et avec les autres entités (un point occupé par une autre entité)
  * En fonction du monde qui l'entour ou d'un ordre du propriétaire
* **Est produit par un bâtiment**:
  * En fonction de sa déclinaison
  * A un coût

Les personnages se déclinent de 3 façons en fonction de leur façon d'interagir avec le monde et chaque déclinaison possède au moins une classe:

* **Melee**: Knight, Champion, Lancer
* **Ranged**: Archer, Wizard
* **Worker**: Worker

##### Type Structure

Une Structure:
* **Est statique**:
  * Placé par le joueur
* **Est conscient du monde qui l'entour**:
  * Zone de perception (Ligne de vue)
  * Gestion des collisions (directes & perception),
  * Mémoire
* **Possède des actions**:
  * En fonction de sa classe
  * Production d'Entités mobiles
* **Est produit par un ouvrier**:
  * A un coût

Il y a 4 classes de bâtiments, chacune spécifique à la déclinaison de personnages qu'elle produit:

* **TownCenter** : Worker
* **Caserne** : Knight, Lancer
* **Archery**: Archer
* **Castle**: Champion, Wizard

##### Type Collectible

<st c="r">ATTENTION: Le type ressource est le nom de l'entité qui fournit ce type de ressource. Ainsi, l'entité Tree de type Ressource permet de récolter la ressource bois.</st>

Un Collectible:
* **Est statique** :
  * Appartient à la carte (Gaïa)
  * Plusieurs instances
* **Possède un type de ressource** :
  * Quantité limitée
  * Récoltable par un ou plusieurs Worker

Il existe 2 déclinaisons de ressources qui gèrent différemment leur points et chaque déclinaison possède une classe:

* **Gold**: GoldMine
* **Wood**: Tree


#### Gestions des inputs

* Clic gauche sur une entité permet d'afficher des informations la concernant.
* Clic gauche maintenu permet de faire un rectangle de sélection qui sélectionne plusieurs entités appartenant au joueur. Si dans la sélection se trouve des entités Structure et Character, la sélection ignore les Structure.
* Clic droit sur une entité sans sélection préalable ne fait rien.
* Clic droit sur une entité avec une sélection:
    * Si l'entité n'appartient pas au joueur, un ordre d'interaction est donné au entités de la sélection.
    * si l'entité appartient au joueur, elle se déplace pour la rejoindre
* Clics droit sur la carte avec une sélection:
    * la sélection s'y rend.
* Déplacement de la camera:
  * pression sur le bouton central de la souris
  * "WASD"



#### Déroulement d'une partie

Cette partie décrit le scénario final 

Au début d'une partie, le joueur se retrouve au commandement d'un bâtiment principale ainsi que une petite troupe ($n$ à définir) d'unités utilitaires. Un certain nombre de crédit (ressource) lui sont alloués. Le bâtiment principal permet de produire de nouvelles unités utilitaires qui elles mêmes peuvent construire des bâtiments de production d'unités offensives ou des bâtiments utilitaires pour augmenter la population. Le bâtiment principal offre une certaine limite de population qu'il est nécessaire de faire augmenter au fur et à mesure de la production d'unités. Cette augmentation de la population se fait par la construction de nouveau bâtiments utilitaires ("maisons").

Pour assurer sa pérennité, il est nécessaire que le joueur investisse des unités utilitaires dans la récolte de ressources qu'il investira dans de nouvelles unités utilitaires ou des bâtiments de production d'unités offensives pour au final amasser une armée suffisante pour détruire le joueur advèrse.

### Conventions

* Le code suit la convention K&R.
* Les noms du code sont écrits en anglais.

## Réalisation

### Architecture

Les premières heures du projet ont été le théâtre de nombreuses discussions sur l'architecture du projet. Cette partie décrit les options considérées ainsi que les choix faits et les raisons qui les motive.

#### Problématique

Dans un projet où de nombreux composants doivent fonctionner de concert, il est primordial de garder un couplage faible entre les différentes parties du code pour qu'il reste maintenable et suffisamment robuste pour pouvoir non seulement supporter toutes les fonctionnalités planifiées mais également être étendu par la suite.

Dans le cadre d'un jeu vidéo cette problématique est au coeur des préoccupations. En effet, la technique du "c'est bon ça fonctionne, c'est que ça doit être une bonne solution" est la meilleure façon de se retrouver plus ou moins rapidement dans un impasse avec un code qu'il n'est pas possible d'étendre car la moindre modification casse un équilibre bancal.

Pour palier à ce problème il est non seulement indispensable d'avoir une bonne conception et une architecture adaptée, mais il faut aussi toujours s'assurer que le nouveau code s'intègre dans l'architecture sans modifier sa philosophie et les mécanismes en place.

>What is **good** software architecture?
For me, good design means that when I make a change, it’s as if the entire program was crafted in anticipation of it. I can solve a task with just a few choice function calls that slot in perfectly, leaving not the slightest ripple on the placid surface of the code.
> http://gameprogrammingpatterns.com/architecture-performance-and-games.html

Même si cette façon de faire réduit à première vue la productivité car elle demande un effort constant de refactoring en plus d'un plan initial à suivre, au final, le gain est conséquent. En effet, <st c="r">REF:il est de notoriété publique qu'un développeur passe la majorité du temps non pas à écrire, mais à lire du code</st> . Sachant ça, chercher a optimiser le temps d'écriture du code serait aussi éfficace qu'un CPU deux fois plus puissant pour résoudre un problème avec un algorithme d'une complexité temporelle élevée à la place de changer d'algorithme. Et de la même façon qu'un algo avec une complexité temporelle élevée serait boosté par un meilleur CPU sur les premières itérations, le problème serait quand même identique sur le long terme. Dans le cas d'une bonne architecture c'est pareil, si le projet est gros et s'étale dans le temps, la solution n'est pas d'écrire plus vite du code, mais de bien l'écrire. Ainsi, à chaque fois qu'il faudra étendre ce code, la phase de compréhension du code dans lequel s'intègre le changement est plus courte et c'est bien là que se trouve le plus grand manque à gagner. 

Le problème d'une série de solutions attachées ensemble pour donner le comportement souhaité à des objets n'est pas tant dans la qualité du code des solution mais dans le façon dont communiquent ces différents morceaux de code. Autrement dit, le couplage y est sûrement très important et le point critique où il n'est plus possible d'avancer approche à chaque ligne supplémentaire. De plus à ce moment là, chaque ajout est un casse tête non pas à cause de la nouvelle fonctionnalité mais pour faire en sorte de premièrement comprendre le code à utiliser et ensuite pour assurer de ne rien casser.

Pour ne pas tomber dans le piège du code rapide, le temps a été pris pour définir les principaux composants de l'application et de quelle façon ils communiquent ensemble ce qui a donné lieu à un premier jet de l'architecture:

![](https://i.imgur.com/vxPMIol.png)

Ce premier jet fût utile pour séparer les tâches mais également pour identifier certains points chauds à étudier. Le principal étant la façon de gérer le module `Entity`. Plusieurs approches ont étés considérés et seront décrites dans les prochains points.

#### Approche héritage

Cette approche est naturelle, et pour peu que les relations et la communication entre les classes qui se chargent des mécanismes de base implémente les bon patterns, le résultat est fonctionnel et relativement robuste. Le problème vient principalement du coté de l'arbre d'héritage des entités.

En effet, dans cette implémentation, chaque entité est un objet. Le système d'instanciation est donc basé sur des classes et permet à une entité d'en étendre une autre tout en jouissant de comportements polymorphiques.

Poussée au maximum, cette façon de faire conduit à une grande hiérarchie de classes rigide où La difficulté de définir la place d'une nouvelle entité est proportionnelle à la taille de la hiérarchie. Le diagramme suivant illustre ce problème:

![](https://i.imgur.com/6htNNE9.png)

Ce projet comportant justement un grand nombre d'entités aux comportements variés et l'expérience de YARTS (avec un seul "A") discalifie cette possibilité.

Pour compenser les défauts de cette approche il est nécessaire de trouver une autre façon que l'héritage pour la gestion des entités.

#### Approche Components (composition)

Dans cette approche, la construction d'entités ne se fait plus via héritage mais via composition tout en tirant profit du polymorphisme en définissant une interface `Entity`. Une entité devient une aggrégation (techniquement une composition, d'où le nom du pattern) de composants et chaque composant encapsule la logique qui le concerne ainsi qu'une référence vers l'entité qui le contient:

![](https://i.imgur.com/L8T7ZC9.png)


Malgré une complexité apparente dans la modélisation, un système de gestion des entités basé sur ce système possède les avantages suivants:
1. Il est aisé de créer une nouvelle entité
2. Possibilité de dynamiquement ajouter ou retirer des composants (et donc de modifier le comportement) à la façon d'un pattern *Strategy*
3. Plus de performance de part une gestion plus optimisée du cache CPU 

Ce pattern ne fait pas partie des patterns classiques du _Gangs of four_ <st c="rgb">REF: https://www.journaldev.com/31902/gangs-of-four-gof-design-patterns</st> et même si il revient dans de nombreuses discussions sur Internet, il est difficile de trouver son origine. Il semble bien ancré dans le domaine du jeu vidéo et celui des interfaces graphiques (plus particulièrement celles développées avec les outils du web) <st c="r">REF: https://medium.com/@nateeborn/component-design-patterns-cbe1bdc7261b</st>. Ce qu'il est intéressant de noter, c'est que même si dans les grandes lignes il s'agit du même pattern, c'est des besoins totalement différents qui justifie son emploi dans chacun de ces deux cas. 


##### Utilisation de Component dans le web

Côté web, c'est principalement pour le côté réutilisabilité des components qu'il est apprécié. En effet, les frameworks frontend VueJS <st c="r">REF: https://vuejs.org/v2/guide/components.html</st> et React <st c="r">REF:https://medium.com/@nateeborn/component-design-patterns-cbe1bdc7261b</st> l'emploie tous les deux. Dans ces deux frameworks, un component est un élément graphique "self contained" (auto-suffisant) qui contient logique, présentation et état dans un seul et même objet qui est utilisé sous forme de composition dans un objet responsable de leurs cycle de vie.


##### Utilisation de Component dans les jeux vidéos

sources:

* [Data locality](https://gameprogrammingpatterns.com/data-locality.html)
* [Sony Computer Entertainement Eu R&D division: Pitfalls of OOP](http://harmful.cat-v.org/software/OO_programming/_pdf/Pitfalls_of_Object_Oriented_Programming_GCAP_09.pdf)
* [Modern C++: What you need to know conf](https://channel9.msdn.com/Events/Build/2014/2-661)

Côté jeux vidéos, c'est pour le gain de performance qu'il offre vis-à-vis de l'approche héritage en accélérant les accès mémoire via optimisation de l'utilisation du cache du CPU. 

<st c="r">CONTAINER: INFO:</st>
"Random Access" veut dire que contrairement à un disque dur, l'accès à n'importe quelle donnée se fait à la même vitesse.

![](https://www.researchgate.net/profile/Christine_Eisenbeis/publication/254014003/figure/fig1/AS:393211676774402@1470760377534/Memory-Access-vs-CPU-Speed.png)

Les CPU ont pendant des années profité d'un gain de puissance exponentiel mais en arrière plan de cette réalité se cache le drame de la RAM dont la puissance ne croit que de façon linéaire. <st c="r">REF: PUBLICATION WOOT https://www.researchgate.net/figure/Memory-Access-vs-CPU-Speed_fig1_254014003</st> Le problème vient du fait que pour qu'un CPU puisse calculer, il doit avant tout sortir les données de la RAM pour la ranger dans ses registres. Et là, tout d'un coup la précédente figure révèle le drame qui se joue lors de chaque calcule de balle qui tombe sur une scène...

Autrement dit, avec les fréquences des CPU modernes le temps d'acquisition d'une donnée en RAM peut prendre plusieurs centaines de cycle CPU <st c="r">REF: https://gameprogrammingpatterns.com/data-locality.html</st>. Pour compenser ce problème, les CPU disposent d'une mémoire intégrée dont l'accès est jusqu'à deux ordres de magnitude plus rapide que pour de la RAM <st c="r">https://softwaretradecraft.com/when-ram-is-too-slow-how-dynamic-in-memory-processing-changes-the-game-for-data-analytics/</st>. Cette mémoire est le _cache_ du CPU et il en existe 3 différents sur les puces modernes: L3, L2 et L1 qui possède chacun un rapport taille/vitesse d'accès différent, L3 étant le plus lent mais le plus volumineux.

![](http://softwaretradecraft.com/wp-content/uploads/2013/10/cache-speedup-2013-e1381868603295.jpg)

Ce n'est pas que le cache est plus rapide pour accéder à la mémoire, mais il applique une stratégie qui consiste à _cacher_ toute la zone contiguë à un byte fetch en RAM par le processeur en pariant sur le fait que le prochain byte qui sera réclamé fera partie de cette zone. Si c'est le cas, le CPU pioche dans le cache, ce qui lui évite de devoir faire un accès RAM. 

![](https://gameprogrammingpatterns.com/images/data-locality-cache-line.png)

Cette zone est appelée _cache line_ et sa taille est de l'ordre de la centaine de bytes. Le fait d'éviter un accès RAM car le cache contient le prochain byte s'appelle _cache hit_ et si ce n'est pas le cas, on parle de _cache miss_ <st c="r">https://www.techopedia.com/definition/6306/cache-hit</st>

Lors d'un *miss*, et vu la <st c="r">FIGURE AVANT LA PRECEDENTE</st>, les performances du CPU s'éffondrent car il se retrouve bloqué en attendant le prochain byte de donnée. L'impacte d'un _cache miss_ est très facile à simuler programmatiquement:

```py
for i in myCollection:
  sleepFor500Cycles()
  i.doStuff()
```

Il est facile de comprendre que dans ce cas l'impacte de la fonction `sleepFor500Cycles()` est dramatique sur le temps d'exécution. Pourtant c'est exactement le même impacte qu'a un _cache miss_ sur les performances d'un programme. La conclusion logique est donc qu'il est critique d'optimiser un programme pour faire en sorte de minimiser les _cache miss_. C'est très précisément à ce moment que le pattern Component rentre en jeu. 

Le pattern Component est un premier pas vers une façon plus efficace d'architecturer un programme. On parle de "Data-Oriented Design" (DOD) <st c="r">REF: https://www.dataorienteddesign.com/dodmain/</st> et cette façon d'architecturer a au centre des ses préoccupations le fait d'accélérer les accès mémoire en optimisant l'utilisation du cache et donc en assurant que **les données sont stockées en mémoire dans l'ordre de leur accès**.

Le sujet est vaste et passionant mais sort du cadre de ce rapport pour plus d'informations sur ce sujet, le chapitre "Data locality" <st c="r">REF: https://gameprogrammingpatterns.com/data-locality.html</st> du livre de XXXXX est une excellente introduction et pour avoir une vue complète de l'état du DOD <st c="r">REF: https://www.dataorienteddesign.com/dodmain/</st>


##### Component, Strategy et Facade 

Au niveau implémentation, ce pattern est très proche du classique *Strategy*. Ces deux patterns ont en commun le fait de déléguer une partie du comportement d'un objet à un autre objet. 

Dans un pattern *Strategy*, la partie déléguée, la stratégie, sera généralement sans état et encapsule uniquement un algorithme. La Stratégie est instanciée par l'objet qui délègue et il est en charge de le remplacer par un autre objet qui implémente un autre algorithme en fonction de l'état de l'application <st c="r">REF: https://refactoring.guru/design-patterns/strategy</st>.

Dans un pattern _Component_, le composant est également une objet à qui on délègue une partie de comportement. À la différence de la stratégie, le component en plus de se charger de la logique, contient également l'état associée à ce comportement. Cette particularité le rends moins générique et crée un certain couplage. Ce qui n'est pas forcément problématique car lorsqu'il est possible de s'en passer un component est simplement une stratégie qu'il est possible d'utiliser dans plusieurs conteneurs. Ces deux patterns sont donc complémentaires et la limite est souvent floue entre les deux.

À titre plus personnel, n'ayant pas trouvé d'article ou d'étude pour corroborer mon impression, je trouve qu'il possède aussi beaucoup du pattern Facade. De la même façon que Facade, Component sert de point d'entré à un sous système complexe dont il abstrait l'utilisation <st c="rgb">REF: https://refactoring.guru/design-patterns/facade</st>.

##### Unity gameobject

<st c="r">SOURCE: Le code d'unity</st>
Au coeur des GameObject d'Unity se trouve une implémentation du pattern Component <st c="r">REF: https://docs.unity3d.com/Manual/Components.html</st>. Un clique sur un objet de la scène dévoile dans l'inspecteur sa composition. Chacun des éléments affiché alors dans l'inspecteur est un component qui représente un comportement et l'état qui va avec.

Dans le cadre de ce projet il semble donc assez naturel de profiter de l'implémentation existante. Mais avant de se précipiter, il reste encore une voie à explorer, celle de la performance sans compromis : ECS.

#### Approche ECS

Une confusion persiste sur le fait qu'Unity utilise une approche ECS. Ce n'est pas le cas. <st c="r">REF: https://docs.unity3d.com/560/Documentation/Manual/GameObjects.html</st>.

ECS se démarque des précédentes approche par le fait que ce n'est pas un pattern OOP. En effet, il n'utilise aucun mécanisme de ce paradigme. Son concept est basé sur les qualités d'optimisation de la mémoire qu'apporte la composition vis-à-vis de l'héritage mais poussées au maximum. Cette façon de faire dégage des performance exceptionnelles pour l'affichage de très nombreuses entités. Cette performance se paye par l'obligation d'adopter sans compromis une approche Data Oriented.

En ECS, les `Entity` sont les objets du jeu et sont **définis implicitement** par une collection de `Components`. Ces Components ne contiennent que des données et sont opérés en groupes fonctionnels par des `Systems`.

##### Component

Un component est un simple conteneur. Une classe qui implémente un component a des attributs mais pas de methode. Chaque component décrit un certain aspect d'une entité ainsi que ses paramètres. En soit, un component, n'est pas grand chose et c'est leur cumule qui est interessant. Voici un exemple de composants:

* `Position(x, y)`
* `Velocity(x, y)`
* `Physics(body)`
* `Sprite(images, animations)`
* `Health(value)`
* `Dammages(value)`

##### Entity

**En ECS, une entité est un concepte**, mais peut être vu comme un objet du jeu. Par exemple, un rocher, une maison ou un soldat. Fondamentalement elle n'est définie que par les composants qui le constitue et un ID. Il est possible d'ajouter ou de retirer des composants pendant l'exécution, ce qui se traduit en une façon fondamentalement différente d'aborder les choses. En effet, dans une vision ECS des choses, on peut imaginer qu'une de nos entité est un mage qui peut geler les soldats adversaires. Ces soldats sont eux mêmes des entités et si ils sont touchés par le sort de glace du mage, il suffit de leur retirer leur component Velocity pour les clouer sur place. À partir des composants précédents il est possible d'images les Entités suivantes:

* `Rock(Position, Sprite)`
* `Ball(Position, Velocity, Physics, Sprite)`
* `Wizard(Position, Velocity, Sprite, Health, Damages)`

##### System

Les systèmes sont le coeur de la logique de l'ECS. Un système opère sur une combinaison de composants spécifique. Par exemple, Le système `MovementSystem` peut opérer sur les entités composées des components `Postion` et `Velocity` et contient toute la logique qui permet de déplacer des entités. Chaque système, et dans l'ordre d'instanciation de tous les systèmes sera mis à jour idéalement 60 fois par seconde. Voici quelques définitions de systèmes:

* `MovementSystem(Position, Velocity)`: Applique une vélocité à l'entité qui possède Position
* `GravitySystem(Velocity)`: Applique une accélération à l'entité qui possède Velocity
* `RenderSystem(Position, Sprite)`: Affiche les entités qui possède Position et Sprite


##### Unity DOTS

Unity DOTS est l'implémentation par unity d'une suite d'outils basé sur une approche DOD. Pour le moment ce package est hautement volatile et uniquement accessible en preview. Dans ce package ce trouve "Entities" qui est le framework ECS que propose unity.


#### Pattern architectural

Le choix a été intialement fait d'utiliser une version programmatique de du pattern "Components". Après plusieurs discussion avec le Pr. Le Callennec, il a été décidé d'adapter l'architecture pour la faire utiliser les Gameobjects.

##### Première itération: Approche programmatique

Le diagramme suivant présente l'architecture au moment de commencer à écrire du code:

![](https://i.imgur.com/FGRo4Vh.png)

Les classes ne sont pas complètes et ne présentent que les membres importants pour que les membres du groupe aient une bonne idée de l'architecture générale et puisse travailler chacun de leur côté. À ce stade, il manque encore plusieurs composants  à l'application mais ces composants utilisant ceux existant, ne nécessiteront pas de remaniement de l'architecture pour être intégrés. Une approche itérative a donc été adoptée pour permettre de commencer à présenter du contenu lors des réunions hebdomadaires avec le Pr. Le Callennec. 

Cette premier itération se base sur l'usage de factories et d'une classe `Scenario` (absente du diagramme précédent) pour instancier les éléments du jeu. Cette approche réduit la dépendance à l'interface d'Unity et ce qui rend le travail plus "traditionnel" pour des développeurs.

Il reste actuellement encore un vestige de cette approche dans les sources du projet, dans le dossier `Scripts\Game\Entities\OLD\` qui contient deux fichiers.

À ce stade, les components de la classe `Entity` n'héritant pas de `MonoBehaviour`, il était de la responsabilité de la classe `EntityFactory` de composer les différents membres de la classe entity pour retourner ce qu'il lui était commandé par la classe `Scenario` qui elle était en charge de placer les entités à différents endroits de la carte.

##### Seconde itération: Approche prefabs

À ce stade la grande majorité des mécanismes nécessaires sont mis en place. Après une discussion avec le pr. Le Callennec il a été décidé de tenter de remodeler légèrement la classe `Entity` pour lui permettre d'utiliser les avantages qu'apporte les prefabs d'Unity. Le principal avantage serait de pouvoir se passer de la classe `Scenario` et composer directement les scènes à l'aide de l' éditeur d'Unity. 

Le premier changement fût le fait de faire hériter tous les components de `MonoBehaviour` afin de les rendre accessible depuis l'inspecteur d'Unity. Cette modification a eu comme conséquences de pouvoir se passer d'une grande partie des factories mais aussi de certaines stratégies. En effet, ces dernières pouvait maintenant se faire remplacer par des membres publiques du component qu'il serait nécessaire d'initialiser depuis l'interface d'Unity.

À ce stade l'architecture a pris sa forme finale qui est illustrée par le diagramme qui suit:

![](https://i.imgur.com/XuvgR0P.png)

Il n'est pas entièrement complèt et certaines choses diffèrent du code actuel mais tous les mécanismes importants y figurent. 

#### Configuration d'Unity

##### URP

Pourquoi t'as fait ça ?????


### Mécanismes

De très nombreux mécanismes existent et il est tout simplement impossible de tout détailler et documenter dans le temps imparti. Pour compenser, **en annexe de ce rapport se trouve plusieurs vidéos de présentation de l'application**.

#### Anatomie d'une Entity

Une entité est représentée par la classe `Entity` qui est le point d'entré de sa logique. Cette classe héritant de `Monobehaviour`, un overload de la méthode `Update` permet d'injecter la logique dans le moteur d'Unity et les différents components qui sont attribués à son GameObject définissent son comportement:

![](https://i.imgur.com/V3CJIeF.png)

Une entité n'est pas un monolithe et est en réalité un conteneur. La figure précédente présente le GameObject à la racine de ce conteneur et la figure suivante décrit la hiérarchie d'une entité:

![](https://i.imgur.com/Tul23je.png)

Les meshs sont contenus dans le GO _Model_ et le GO _Extensions_ contient un nombre variable d'extensions en fonction de l'entité qui sont la partie graphique de certains Components:

* Le GO **HealthBar** contient la présentation des components qui spécialisent le component `BaseResourceComponent` (Dans le cas du Knight `HealthPointsResource`).
* Le GO **Projection** contient le marqueur de selection utilisé par le component `SelectableComponent`.

![](https://i.imgur.com/6A7nPm8.png)


Le dossier `Resources/Prefabs/Entities`. Contient toutes les entités du jeu. À la racine de ce dossier se trouve la prefab la moins spécialisée qui est à l'origine de toutes les autres spécialisations. Cette façon de faire permet de ranger dans cette prefab toutes les extensions qui sont communes à toutes les entités. 

#### Particules

Tous les impactes génèrent des particules. Ces particules sont instanciées programmatiquement par la classe `FXFactory`. Ces effets viennent de l'asset "" 

SCREEN BLOOD

![](https://i.imgur.com/gu79Hub.png)


#### Meshs

Orientation du sang tout çà

#### Projectiles

#### Destruction

#### Brouillard de guerre

![](https://i.imgur.com/UhQ1Xfg.jpg)

Pour le projet de M. Seuret, ce dernier se concentrait majoritairement sur le _Fog of War_ (brouillard de guerre). En effet, bien que paraissant très simple, il est complexe de créer un brouillard de guerre fluide ne consommant pas trop de ressources.

Ce point utilise des notions du chapitre 8 du livre "WebGL par la pratique", concernant les textures.
En effet, que ce soit les ajouts des meshs à une texture où l'application de cette texture projeté sur l'entièreté du terrain, ce concept est dépendant de la possibilité de texturer l'objet.

Cette partie du projet est concentrée sur le rendu graphique. En effet, le FoW n'a aucun autre intérêt que de cacher de l'information au joueur. Il existe quand même un besoin pour la modélisation géométrique qui nous permet de créer les meshs simulant le champ de vision des unités. Cependant, cela reste très simple étant donné qu'il ne s'agit que de plans.

##### Solution proposée
Suite aux recherches effectuées ainsi que les différentes méthodes d'implémenter un FoW dans Unity, il a été décidé d'implémenter une méthode d'addition d'images.

Il existe cependant d'autres possibilités dont voici les désavantages : 
* Texture transparente surplombant l'entièreté de la carte
    * Celle-ci doit prendre en compte l'angle de la caméra afin de supprimer les parties de la texture qui nous permettront de voir sur le terrain. 
* Voxels supprimés à l'approche des unités
    * Extrêmement coûteux à initialiser 
    * Demande le rajout de voxel lorsque ces derniers ne sont plus dans le champ de vision des unités afin de simuler les parties découvertes.
* Brouillard de Unity
    * Effet global
    * Effet coûteux pour le GPU

Ainsi, étant donné que tous les calculs sont fait sur des images, peu importe le nombre d'unités ou structures présentes, le processus ne prendra pas de temps supplémentaire (très peu, dû à la génération des meshs de chaque unité). Ceci était un point crucial car le projet a pour but de simuler aisément des batailles de plusieurs centaines d'unités.


##### Modèle développé

###### Fog of War standard
Prenons tout d'abord le FoW basique, c'est-à-dire ce que les unités voient actuellement.
Premièrement, il est nécessaire pour les unités de générer un mesh de leur simulant leur champ de vision théorique. Ainsi, ceci permettra de déterminer ce qu'elles peuvent voir. 
![mesh des unités](https://i.ibb.co/J3tbbX3/image.png)
Ces meshs sont ensuite capturés par une caméra - capturant son propre layer Unity - qui générera une _Render Texture_. Cette dernière sera ensuite projeté sur le terrain grâce à un objet de projection. Ce dernier ne pouvait malheureusement pas être utilisé dû au choix, en amont, d'utiliser le _Universal Render Pipeline_ de Unity. 
Pour résoudre ce problème, nous avons utilisé le shader développé par Colin Leung: [UnityURPUnlitScreenSpaceDecalShader](https://github.com/ColinLeung-NiloCat/UnityURPUnlitScreenSpaceDecalShader).

![Explication FOW](https://i.ibb.co/SKDF9K1/image.png)

Ceci nous permet finalement de rendre totalement noir l'entièreté du décor qui n'est pas directement vu par une des unités.

###### Fog of War découvert
Concernant le FoW semi-transparent, c'est-à-dire les lieux découverts mais sur lesquels nous n'avons plus la vision, cela fut plus complexe.

En effet, le principe utilisé précédemment ne permet pas de garder une trace des positions déjà découvertes. Il est néanmoins possible d'adapter la solution. 

Une des solutions est de ne pas _clear_ (mettre à zéro) la texture dans laquelle la caméra va sauvegarder ses informations. Cependant, vu que nous utilisons le _Universal Render Pipeline_, cette solution n'est pas disponible. 
Il existe cependant la possibilité d'empiler (stack) le rendu de caméra et, pour les caméras empilées sur celle de base, de sauvegarder **par-dessus** les informations de la caméra de base. Il n'est malheureusement pas possible d'avoir le même comportement depuis une texture.

![Explication FoW découvert](https://i.ibb.co/DD0p5Nn/explication-fow-Discovered.png)

Ainsi, pour contourner les limitations de Unity, une render texture a été placée dans un layer propre à elle ainsi qu'une caméra. Le seul but de cette caméra est de filmer la texture afin de la récupérer pour la caméra qui sauvegardera par-dessus. Une fois la texture récupérée par la caméra de base, la caméra suivante sur la pile pourra sauvegarder ses informations et ainsi mettre à jour la texture. Cette dernière sera ensuite réappliquer sur l'objet filmé par la caméra de base et le processus recommence.


##### Integration

Comme expliqué précédemment, il est nécessaire d'utiliser un ensemble de 3 caméras : 
* Caméra pour le FoW standard
* Caméra qui lit la texture
* Caméra qui applique le FoW par dessus la texture

Ensuite, il est nécessaire d'ajouter des objets représentant le FoW en lui même. Il s'agit de cube très fin sur lesquels le shader récupéré agira.

##### Avantages et inconvénients
Concernant les avantages et inconvénients de cette implémentation, elle impose un coût presque fixe en performance, ce qui peut être et un avantage, et un inconvénient dépendant du contexte.
Un autre avantage est qu'il prend avantage de ce qui est offert par Unity et demande extrêmement peu de code pour notre rendu. Ici, il n'existe qu'un script permettant de générer les meshs des unités ou des bâtiments. Ainsi, elle est très simple à implémenter.

##### Résultats et performances

Prenons une scène relativement simple, en faisant combattre une vingtaine d'unité de chaque camp : 

![fps combat](https://i.ibb.co/tHBhYh2/image.png)
Comme nous pouvons le remarquer, nous avons une moyenne de ~21 fps pour cette scène de combat (chiffre vert).

Maintenant, en appliquant le FoW : 

![fps combat FoW](https://i.ibb.co/vdNNTn0/image.png)
Cette fois, nous avons une moyenne de ~20 fps, soit une baisse de 5%.
Cependant, il est important de noter que le nombre d'image par seconde est extrêmement volatile et dépend de beaucoup d'autres facteurs. De ce fait, le FoW a un impact quasi nul sur les performances.

Notre implémentation est donc plus que satisfaisante, car il n'impose qu'une perte très minime sur les performances du jeu, et ce avec un facteur de complexité très faible. Ceci nous permet de garder des performances raisonnable même avec un grand nombre d'unités, ou en tout cas que le FoW ne soit pas le point faible de ce projet.

<st c="r">PARLER DES VERSION BUILD</st>


##### Améliorations

Dans l'ensemble, l'implémentation actuelle remplit son travail et permet de ne pas imposer un coût trop élevé en performance. 
Cependant, afin d'être parfaitement fonctionnel, il nécessite encore quelques modifications afin d'ajouter la gestion logique de ce FoW. En effet, il serait nécessaire de n'afficher que les bâtiments découverts dans la zone de FoW découvert (semi-transparent). Les unités ne devraient plus être visibles ainsi que toutes modifications futures de l'adversaire.

##### Références
Malheureusement, il existe très peu de documents parlant de l'implémentation des Fog of War dans les jeux vidéos. 
De plus, ces derniers sont en général relativement vagues sur les techniques utilisés ou se reposent sur les possibilités offertes par les moteurs de jeu (Unity, Unreal Engine).

Il est néanmoins possible de trouver des explications de FoW plus poussé mais ces derniers se concentrent principalement sur la partie logique. Voici un exemple avec le "FoW" de [Valorant](https://technology.riotgames.com/news/demolishing-wallhacks-valorants-fog-war), nouveau jeu de Riot Games encore en bêta. 

Ce système est déjà utilisé dans le jeu Counter-Strike : Global Offensive sorti en 2012 et existait déjà aussi tôt que 2005 dans des mods de son itération précédente, Counter-Strike : Source.

Cependant, ceci ne nous intéresse pas étant donné qu'il ne s'agit que de la partie logique et non pas la partie graphique d'un tel rendu.

Chamberlain, Paul. “Demolishing Wallhacks with VALORANT's Fog of War.” Riot Games Tech, Paul Chamberlain, 14 Apr. 2020, technology.riotgames.com/news/demolishing-wallhacks-valorants-fog-war.


### Design

#### Entités

#### Animations

### Interface utilisateur

#### Architecture

#### Panneaux

##### Sélection

##### Actions

##### Mini-map

##### Ressources

## Récapitulatif

### Objectifs

### Bugs

### Améliorations

## Conclusion






## Assets

Le fait est que nous aimons Unity et nous avons investi dans certains tools et packs d'assets.

### Tools

#### Free
* Debug Drawing
* LiteFPSCounter
* Icon Maker
* ProBuilder
* ProGrid
* PolyBrush

#### Payed
* Peak
* Console Pro
* QHierarchy
* Rainbow Folders

### Assets

#### Payed
* ToonyTinyPeople
* Tarbo-FantasyVillage
* Polygonal Arsenal



## Références

### Solide
* [Rts group movement](https://sandruski.github.io/RTS-Group-Movement/)
* [Fog of war](https://andrewhungblog.wordpress.com/2018/06/23/implementing-fog-of-war-in-unity/)
* [Optimisation](https://www.habrador.com/tutorials/unity-optimization/)
* [Profiler](https://makaka.org/unity-tutorials/optimization)
* [UI System architecture](https://www.gamasutra.com/blogs/YankoOliveira/20180108/312617/A_UI_System_Architecture_and_Workflow_for_Unity.php)
* [ECS Deep Dive](https://rams3s.github.io/blog/2019-01-09-ecs-deep-dive/)
* [ECS & Job System](https://www.reddit.com/r/Unity3D/comments/9mxyrk/unity_ecs_and_job_system_in_production/)
* [AI Navigation - unity cookbook](https://books.google.ch/books?id=UMhsDwAAQBAJ&pg=PA603&lpg=PA603&dq=unity+navmesh+steering&source=bl&ots=hvvr3CJGkI&sig=ACfU3U2-SQqlOh2z3ZdBIRgFJl_NX6XU5A&hl=fr&sa=X&ved=2ahUKEwjdtYfF5YjoAhWByqQKHU7FAqM4ChDoATAHegQIBRAB#v=onepage&q=unity%20navmesh%20steering&f=false)
* [paper: design pattern & best practices in Unity](https://dl.acm.org/doi/pdf/10.1145/3077286.3077322?download=true)
* [Gangs of four](https://www.journaldev.com/31902/gangs-of-four-gof-design-patterns)
* [Component design pattern](https://medium.com/@nateeborn/component-design-patterns-cbe1bdc7261b)
* [Sony Computer Entertainement Eu R&D division, pitfalls of OOP](http://harmful.cat-v.org/software/OO_programming/_pdf/Pitfalls_of_Object_Oriented_Programming_GCAP_09.pdf)
### Architecture

* [Game Programming patterns](http://gameprogrammingpatterns.com/)
* [Design patterns in Unity](https://www.patrykgalach.com/2019/05/06/design-patterns-in-unity/?cn-reloaded=1)
* [Message bus pattern](https://github.com/franciscotufro/message-bus-pattern)
* [Messaging Architecture in Unity](https://medium.com/@tkomarnicki/messaging-architecture-in-unity-6e6409bdda02)
* [Unity events vs C# events](https://itchyowl.com/events-in-unity/)
* [Refactoring.guru](https://refactoring.guru/design-patterns)
* [Raywenderlich: intro de component based architecture](https://www.raywenderlich.com/2806-introduction-to-component-based-architecture-in-games)


### Specific

* [URP light shadows](https://answers.unity.com/questions/1690727/universal-render-pipeline-point-light-shadows.html)
* [Ballistics](https://gamedev.stackexchange.com/questions/71392/how-do-i-determine-a-good-path-for-2d-artillery-projectiles/71440#71440)

### Généraliste

* [Habrador: tutos Unity](https://www.habrador.com/)

### Youtube

* [GameGrind - Enemy Attack and aggro](https://www.youtube.com/watch?v=Bs0rJEkYBvc)
* [Quill18reates - Sélection](https://www.youtube.com/watch?v=OOkVADKo0IM)
* [Unity DOTS tutorial (fr)](https://www.youtube.com/watch?v=Y33Hc9raS_Y&list=PLVko8yLzEsQJStJ15W5rYQEuEY9erJqIr&index=8)
* [Unity DOTS CodeMonkey](https://www.youtube.com/playlist?list=PLzDRvYVwl53s40yP5RQXitbT--IRcHqba)
* [DOTS 0.4.0](https://www.youtube.com/watch?v=aw4AkdJIF2k&list=PLS6sInD7ThM3L4AtxjixzA-3vRUjAssa4&index=4)