# RPG-Prototype

This is a project made in Unity meant to be a framework from which an RPG can be built. It includes some of the common elements of an RPG such as Health bars, abilities, combat, inventory system, quest system etc. This project is for educational purposes and offers an example of how to build a framework within Unity.


The aim of this project was to take advantage of Unity's component system to create a modular framework that is easily built upon for future iterations. To accomplish this I conceptualized a few systems.

Entity
An Entity is any Player, NPC, or enemy in the game. The Entity component acts as an interface for interacting with an Entity. The Entity component comes with several delegates that sibling components can subscribe to. This allows for a greater level of modularity as components can be focused on a single delegate and that component can be attached to several entities who react to certain outside stimuli the same way. Some of the delegates that the Entity component comes with are UpdateHealth, UpdateMana, TakeDamage, etc.

Event System
The event system that I created uses ScriptableObjects to store events that would happen in the Game. Scriptable objects are assets and can be added to different gameobjects so I wanted to take advantage of that to decouple certain events. To accomplish this I use 3 classes,
  -Game Event Listener: Encapsulates a UnityAction and stores the events. It is templated to allow for functions with any signature to be stored. Dangerous though as there is no         type checking.
  -Game Action: Scriptable object that stores Game Event Listeners without parameters
  -Game Event: Scriptable object that stores Game Event Listeners that have parameters
  
 Inventory System
 The Inventory system was constructed by first creating an Item class that all items will inherit from such as Weapons, armor, etc. I made Inventory an Interface and created a PlayerInventory component that Implements it. The PlayerInventory component subscribes to a few gameevents for notifying the world that the player has added or removed an item from their inventory. This allows for a questing system for example to be notified if a player has received a specific quest item. Items are stored as SerializedObjects and ItemRefs are used to reference certain items.
 
 AI
 AI is controlled using a state machine/behaviour tree hybrid. I wanted something simple yet robust and although I haven't' worked out all the kinks of the system it has served my project well.
 
 State Machine
 The State Machine uses a Blackboard to store key information about the entity or information about the outside world known to the entity. States define the Type of actions that an entity can take and Conditions are used to change between states. I also added a priority field to states that I can hopefully make dynamic depending on the status of the Entity.
 
 Stats
 Stats are split into multiple fields to allow for greater modularity and decoupling. Stats are split up into three categories, base, augments, and constants. Base is the base stats of the entity. Augments are anything that are to alter the stats of the entity. This can be an attack buff or a poison status. Constants are the game level constants set by me.
 
 Status Effects
 Status Effects can range from buffs to ailments such as Burn. They can be timed or single use. They are stored as scriptable objects to allow for greater modularity. You can add status effects to items or even abilities.
 
 Skills
  Skills are abilities that can be used by Entities. They can be attack based skills, or magic based, etc. Skills are stored as ScriptableObjects to allow for re-usability. One skill can be used by different Entities.
 
 UI
 I payed extra careful attention to decouple UI from business logic. I use the MVC pattern to accomplish this. All my UI classes have delegates that are meant to act as callback for when a certain action is made. The InventoryViewUI for example has delegates for opening, selecting an item, removing items, etc. An InventorController class stores the InventoryView and Inventory interface and handles all the business logic.
 

