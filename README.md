# YAARTS

## Développeurs

* Nathan Latino
* Tristan Seuret
* Sol Rosca

## Sujet

YAARTS: Yet Again Another Real Time Strategy (Game). Ce projet est une tentative d'aller le plus loin possible dans le développement d'un jeu de stratégie en temps réel (Dune 2, Command & Conqueres, Warcraft, Starcraft, ...). Le style visuel visé est Lowpoly/Cartoon.

Le projet est partagé entre les cours d'infographie et de .NET. La partie infographie se concentre sur la partie visuelle ainsi que l'utilisation du moteur Uity 3D et la partie .NET sur la qualité du code C#.

## Cahier des charges

### MVP

* Une carte ("plateau de jeu") sur lequel se passe l'action (prototype)
* Deux factions symétriques
* Entités:
    * Batiments de production
    * Unités de production/récolte
    * Unités offensives
* IA des entités:
    * Pathfinding
    * Collision avoidance (gestion des obstacles mobiles dans le pathfinding)
    * Etats: Idle, moving, engaging, engaged, dead en fonction du contexte
    * Gestion de la ligne de vue et de la portée
* Mecanisme de selection des entités (simple et multiple)
* Gestion des ressources
* Brouillard de guère
* GUI (prototype)
    * Menu principale
    * Une interface utilisateur qui contient les éléments suivants:
        * Une mini carte
        * Affichage des ressources
        * Affichage de la population
        * Panneau de selection
        * Panneau des actions de la selection
        * Curseur intelligent dont la forme dépend du contexte

### Si le temps le permet

* IA qui joue l'adversaire
* Diversifications des cartes de jeu
* Génération procédurale de cartes
* Éléments de gameplay (technoogies, amélioration des unités, variété dans les unités)
* GUI moins prototype

## Base de travail

Nathan Latino et Sol Rosca on fait une première tentative d'implémentation d'un RTS (YARTS) en 2e année en utilisant Java et Libgdx. Bien que YARTS (avec un "A") était entièrement 2D en vue topdown, une base de réflexion concernant les points chauds existe déjà. Le rapport de ce projet est accèssible [ici](https://github.com/nathanlatino/yarts/blob/master/doc/Rapport-YARTS.pdf).

LibGDX n'étant pas un moteur de jeu mais un framework, les philosophies de ces deux technologies ne sont pas comparables. LibGDX ne comporte aucune interface et est relativement bas niveau, particulièrement bas niveau en comparaison avec Unity. Dans ces circonstances il est ardu de pouvoir spécifier ce qui vient directement de ce premier projet autrement que le nom ainsi qu'une première expérience dans le développement d'un jeu video.

Depuis l'introduction à Unity, de nombreux petits prototypes de mécanismes ont été mis en place pour servir de base concrète lors de la familiarisation avec Unity. Le projet actuel utilise les connaissances acquises lors de ces expérimentations mais reppart de zero. En effet, les premiers prototypes n'ont pas été pensés pour pouvoir s'intégrer dans une architecture complète et les reprendre en tant que tel aurait posé de nombreux problèmes.

## Technologies

Le projet utilise le nouvel Universal Render Pipline (URP). Il permet une utilisation plus flexible et aisée du postprocessing ainsi qu'un gain de performance dans de nombreuses situations.

## Assets utilisés

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

## Architecture

Une longue discussion à été mise en place au sujet de l'architecture. Initialement on hésitait très fortement pour partir sur unity DOTS et un pattern architectural de type ECS. Mais finalement, suite à la lecture de nombreuses ressources et entre autres l'excellent livre "Game Programming Patterns" de Robert Nystrom, nous avons décidé d'épouser une architecture de type "Component" qui est l'architecture qu'utilise Unity pour ses GameObjects. Nos entités seront des conteneurs à components.

### Pattern architectural
Le diagramme suivant présente l'architecture qui a finalement été décidée:

![](https://i.imgur.com/FGRo4Vh.png)

Les classes ne sont pas complètes et ne présentent que les membres importants pour permetre le travail en groupe.

Nous avons ici le coeur de la logique. La partie inputs et ses différents niveaux d'abstraction sur la gauche, au centre le système de selection et d'entités qui sont intimement liés par un pattern Composit. Les `Entity` elles-mêmes qui utilisent le pattern Component (Pattern plus spécifique aux jeux vidéos qui ne fait pas partie des patterns du gang of 4).

Nous n'avons que peu d'expérience sur la partie GUI de part nos prototypes et il nous est présentement impossible de réelement l'architecturer. Donc nous adoptons une stratégie qui consiste à l'avoir en tant que module faiblement couplé et de tatonner. Une fois les principales difficultés ciblées, une discussion est prévue afin de mettre en place un refactor de cette portion du code pour mettre en place une architecture plus extensible

La classe `Entity` comporte des membres (Components) dont le type de retour est suffixé d'un `?` cela veut dire que ces membres peuvent être null. Les différents components sont sur la droite du diagramme.

La communication se fait principalement par le biais d'un pattern Observer pour réduire le couplage et favoriser l'ajout de nouvelles fonctionnalités.


### IA des entitées

les entités aussi bien statiques que mobiles ont leur état régi par la machine d'état suivante:

![](https://i.imgur.com/fKvpIJg.png)

Cette machine d'état pilote entre autres le component `Animation` qui est en charge de la communication avec le système *Animator* d'Unity. Cette façon de faire nous permet d'éviter certaines limitations d'*Animator*.

De façon général, une approche code est privilégiée.

Par exemple, c'est notre code qui pilote Animator et non l'inverse, nous sommes d'avis qu'il est plus facile d'avoir toute l'information sous les yeux sans devoir cliquer à gauche et à droite dans l'UI d'Unity à la recherche d'une information. Après lecture sur le sujet, cette approche semble privilégiée pour de nombreux projets pour la raison évoquée.

Un autre exemple sont le fait que nos prefabs ne contiennent que le stricte minimum, et **nos** components ne sont (généralement) pas des dérivés de `MonoBehaviour`. Ils sont tous instanciés dans une classe factory spécifique qui nous permet un controlle plus fin mais surtout moins rigide sur les propriétés de nos entités. Le principal avantage de cette façon de faire est d'éviter de se retrouver à batailler avec le système de *variants* et nested prefabs qui malgré tout reste un outil très haut niveau avec les limitations d'un système haut niveau. De plus cette façon de faire permet de ne pas avoir des tonnes de scripts dans l'inspecteur d'unity. Le revers de la médaille est un gain de complexité pour faire des tests rapides sur les valeurs de certaines propriétés mais rien d'insurmontable.

Les *Scriptable objects* ont étés envisagés mais le temps étant limité nous n'avons pas souhaité creuser cette piste étant confiants dans notre approche.

## Ressources utilisées

* Unity learn
* **Youtube:**
    * [Code monkey](https://www.youtube.com/channel/UCFK6NCbuCIVzA6Yj1G_ZqCg)
    * [Brackeys](https://www.youtube.com/user/Brackeys)
    * [Sebastian Lague](https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ)
    * [EngiGames](https://www.youtube.com/channel/UCbAsfBmEHQpPERAVx8DHxZA)
    * À compléter
* **Architecture:**
    * [Game Programming Patterns (livre en ligne)](http://gameprogrammingpatterns.com/)
    * [Design patterns in Unity](https://www.patrykgalach.com/2019/05/06/design-patterns-in-unity/)
    * [Refactoring guru](https://refactoring.guru/)
    * [ECS Deep dive](https://rams3s.github.io/blog/2019-01-09-ecs-deep-dive/)
    * [Messaging architecture](https://medium.com/@tkomarnicki/messaging-architecture-in-unity-6e6409bdda02)
    * [Message bus pattern](https://github.com/franciscotufro/message-bus-pattern)
