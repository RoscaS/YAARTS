# Semaine du 16 mars

### Objectifs

* Amélioration du débug et des scénarios de tests
* Recherche de solution pour le problème de collisions des flèches (ignore collision global)


### Fait

* Rédaction d'une description de l'architecture
* Rédaction d'une description de l'IA des entités
* Rédaction d'une clarification concernant le projet YARTS (1 "A") fait sur Java l'an passé
* Liste des assets utilisés
* Amélioration du débug et des scénarios de tests

### Reste à faire

* Recherche de solution pour le problème de collisions des flèches (ignore collision global)

### Analyse du problème des flèches

Le problème vient du fait que `Physics.IgnoreCollision` prend deux `collider` et que`Physics.IgnoreLayerCollision` prends deux layers.

Une fleche (prefab appartenant au layer _Projectile_) tirée par une entité de l'équipe A (appartenant au layer _A_) devrait ignorer toutes les entités de l'équipe A et réagir aux collisions de l'équipe B (appartenant au layer _B_).

Comme les deux methodes sont globales, si au temps $t_1$ une entité de l'équipe A fait un call à `IgnoreLayerCollision` avec en argument _Projectile_ et _A_ et qu'au temps $t_2$ une entité de l'équipe B fait un call à `IgnoreLayerCollision` avec en argument _Projectile_ et _B_, toutes les flèches ignoreront toujours toutes les entités appartenant aux layers _A_ et _B_.



# Semaine du 23 mars

### Objectifs

* Recherche de solution pour le problème de collisions des flèches (ignore collision global)
* Mise en place du système de collection de ressources
* Refactor Entity Factory


### Fait

* Problème de collision des flèches résolu
* Refactor d'`EntityFactory` & création d'un prefab `EntityFactory` plus générique que le précédent
* Création d'une `Entity` "Worker" + logique d'attaque spécifique (fondamentalement, un ouvrier attaque un minerai ou un arbre)
* Création d'un préfab + `Entity` "GoldPatch"
* Mise en place des bases du système de récolte de ressources

### Reste à faire

* Génériser le système de récolte de ressources
* Trouver un moyen plus élégant de gérer l'absence de certain components lors des actions


### Bases du système de récolte de ressources

![](https://i.imgur.com/ez0NVYS.png)

Situation initiale

![](https://i.imgur.com/UJQaOaV.png)

Après selection des ouvriers et clique droit sur le carré de minerai


# Semaine du 30 mars

### Objectifs

* Finition du système de récolte de ressources
* Diversification des ressources (or et bois)
* Système de construction de bâtiments


# Semaine du 30 mars

### Objectifs

* Correction de toutes les erreurs liées au tir de flèches
* Refactor du système de stratégies d'attaque
* Généralisation du système d'attaque/vie:
  * Quand une entité offensive clique droit sur une entité offensive ennemie, elle l'attaque
    * Entité fait des dégâts et possède une spécialisation du système généralisé qui concrétise sa vie
  * Quand une entité de production clique droit sur une ressource, elle la récolte
    * la ressource utilise une spécialisation du système généralisé qui concrétise ses ressources totales
* Système de fade des corps (+ sang) après un certain temps.

### Fait

* Correction des erreurs générées par les flèches tirées au moment où la cible est tuée.
* Correction du bug qui faisait spawn des flèches en boucle si l'entité était déplacée au moment de tirer.
* Il n'est plus possible de spammer le click droit pour attaquer en ignorant le cooldown
* Mise en place d'un système d'atlas (sous forme de dictionnaire statique où les valeurs sont une structure contenant les données liés à une entité concrète) de préfabs dans la classe `EntityFactory`.
* Remplacement de l'interface `IAttackStrategy` par une classe abstraite `AttackStrategy` pour éviter la répétition de code dans les méthodes `Attack` et `AttackCallback`.
* Refactor du code lié aux attaques qui tendait à s'étaler dans `State`, `AnimationComponent` et `Entity`. Tout est maintenant exclusivement des les concrétisations de `AttackStrategy`
* Mise en place d'un système de fade des corps. Après n secondes un corps commence une translation vers y négatif pour disparaitre sous le sol. Une fois la translation effectuée, l'entitée est détruite et tout le sang qu'elle a versé sur le champ de bataille fade.

### Reste à faire

* Généralisation du système d'attaque/vie


# Semaine du 06 avril

### Objectifs

* Généralisation du système d'attaque/vie:
  * Quand une entité offensive clique droit sur une entité offensive ennemie, elle l'attaque
    * Entité fait des dégâts et possède une spécialisation du système généralisé qui concrétise sa vie
  * Quand une entité de production clique droit sur une ressource, elle la récolte
    * la ressource utilise une spécialisation du système généralisé qui concrétise ses ressources totales
* Finition du système de récolte de ressources
* Diversification des ressources (or et bois)
* Système de construction de bâtiments


### Fait

* 06/04: Refactor CameraRig
* 06/04: Sticky arrows follow body animation
![](https://i.imgur.com/Bthu340.png)

* 07/04: Wizard, FireBall & bump effect + overall optimisation
![](https://i.imgur.com/Jeo3bHC.png)
Build v0.7: 200 vs 200 with 3440x1440, 4x MSAA @ 32fps
* 07/04: Animations: fine tunning
* 07/04: Mise en place d'effets de particules
* 08/04: [Destructeurs/finalizers C# et Unity](https://sometimesicode.wordpress.com/2019/09/08/cs-finalizer-destructor-trap/): Assurer la destruction des objets non `MonoBehaviour` membres d'un objet `MonoBehaviour`
* 08/04: Refactor Factories + création de Data Objects.
* 09/04: Refactor `EngagingComponent` (remplacé par une généralisation: `InteractionComponent`) & `HealthComponent` (remplacé par ResourceComponent). `InteractionComponent` Utilise des stratégies qui encapsulent les interaction possible de l'entité (fournies à la construction).
* 10/04 Classe utilitaire pour faire pop des textes à la volée:
![](https://i.imgur.com/fKOAu9r.png)
* 11/04 Units Spawner: Permet de faire spawn facilement toutes les entités du jeux pendant l'execution.
* 14/04 Les `GameObject` dynamiques ne sont plus instancés à la racine de la hierarchie mais dans des GO spécifiques.
* 14/04 Le sang coule dans la direction de la frappe sur la cible (Melee et distance).
* 14/04 Ajout d'un système de gestion de la vitesse du temps qui passe (Slow motion).
* 16/04 Optimisation des effets de particules et mise en place d'un système d'object pooling pour les projectiles
* 16/04 Ajout d'un effet particulier lorsqu'un mage tue une entité (en plus de l'explosion):

![](https://i.imgur.com/LUZ0LB0.jpg)

* 17/04 Création du `Collectable` Tree (Type d'entité variant des `Characters` uniquement par sa composition de `Components`). Peut être collecté par les ouvriers. Lorsqu'un ouvrier le touche avec sa hache, des particules analogues au sang sont émises (copeaux de bois), une animation le faisant vaciller est jouée et l'ouvrier puise un certain nombre de points de ressource de l'arbre. De plus, aléatoirement, lorsqu'il est collecté, des feuilles tombent de l'arbre. Une fois que les ressources de l'arbre sont épuisées, ce dernier chute au sol et laisse sa place à une souche. Au bout d'un certain temps, l'arbre disparaît:

![](https://i.imgur.com/Ene5SjL.png)

* 17/04 Diversification aléatoire lors de l'instanciation d'un `Tree`. 4 Modèles different sont possibles en plus d'une rotation aléatoire et de légers tweaks sur la scale.

* 17/04 Génération procédurale sommaire d'une forêt:

![](https://i.imgur.com/bSk2x2M.png)

![](https://i.imgur.com/ALazJ3O.png)

* 18/04 Mise en place d'une carte "jouable":

![](https://i.imgur.com/fDYI9vR.jpg)

* 19/04 Mise en place des entités de type batiment + gestion des dégats:
![](https://i.imgur.com/k5ilBx3.png)
* 20,21,22/04 Intégration + optimisation du Fog of War:
![](https://i.imgur.com/UhQ1Xfg.jpg)

* 23/04 Remplacement des noms de classes, types d'entités et type de joueurs par des énumérations
* 25/04 + 26/04 Refactor du système de components pour un système qui s'intègre mieux dans l'écosystème Unity.
* 28/04 Si elles ont le choix les unités offensives favorisent l'attaque d'entités mobiles vs les structures:
![](https://i.imgur.com/7Vc13xM.png)











