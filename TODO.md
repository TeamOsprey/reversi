## 2021-07-27
- work with the board file to read the currect state of the board
	- we decided not to proceed with this as it can work out of memory
- eventually accept the next position from IO

## 2021-08-03
- Review the last two tests. We implemented that knowing it is not going to be the expected output after flipping is implemented.
- Review the lists (black, white, and blank) and make sure our new implementation impact their correctness.

## 2021-08-10
- [X] We should review Direction class, maybe add property for each direction, or use Dictionary again
- [X] We talked about collecting squares on each direction, and either flipping them or disposing them as we reach the end of the path

## 2021-08-17
- We refactored Direction class and added properties for each direction
- We refactored Game, made it easier to read and ready for accepting directions
- We ended RED. Maybe we want to comment out the last test until we are done with following refactoring.
- [X] Change properties of Direction class from int[] to a new type (e.g. Vector {Row, Column}). We need to think about the name for the new type.
- [X] We can change the Board to collection of Squares
- [-] We can add an ADD operation to Square class
		-- We attempted this and realized that it will not clean or simplify the code.
- [X] We can implement the logic to check different directions 


## 2021-08-24
- We commented out the failing test
- We replaced char[,] with Square[,] in Board and made the changes in other classes
- We move UpdateSquare from Game to Board
- [X] Now that Square has a new property as Colour, maybe we should revisit list of White, Black, and check for possibility of improvements.

## 2021-08-31
- replaced Int[] within Direction with a new Vector classs.
- updated lists to use existing squares instead of creating new squares.
- Reconsidering the feasability of line 20 creating an ADD operation to Square class.

## 2021-09-14
- We tried the idea of adding operator to square to go to different direction. That didn't work as for navigate on board we need access to the board 
but in Square class, where we can define operator method, we don't have that information.
- We also added a test that required going to UP direction but that failed
- We noticed there are some uncertainity on how directions are defined. We fixed UP and DOWn but the rest of directions must be fixed.
- We ended up with most of tests failing and that's related to how navigation on board is defined.

## 2021-09-21
- We fixed all directions
- We got rid of _blackCounters and _whiteCounters lists
- We retired one GetNextSquare in Game, replaced it with Board.Add
- [X] We should revisit list of Blank, and check for possibility of improvements

## 2021-09-28
- We replaced _blanksqures list with Board.GetBlankPosition using a LinQ statement.
- We are examining other options for capturing counters.

## 2021-10-05
- We covered place counter for all directions and flipping of the captured counters
- [X] Add the logic to check for first 4 legal moves: 
		* if the board is empty, only one counter can be placed in one of the legal 4 central squares
		* if there is aone counter in the middle 4 squares, the opponent can place in 3 remaining middle squares
		* continue until all 4 counters are placed
- [X] Recognize the PASS turn
- [X] Develop the Consol app to play by sending commands


## 2021-11-02
- We started working on Pass rule and finished in GREEN state!
- Next week we will need to start with checking the rules about when and how to Pass
	- Shall we only inform the player or give option to initiate thePass?

## 2021-11-09
- We agreed that setting the status of Game doesn't belong to the class' constructor
- We talked about some redesign before we continue
	- We may need to set the status of Game right inside the PlaceCounter method. So we won't wait for the next player to make the move before we find out what status is.
	- [x] We will probably need a method to give us the legal positions by providing it the player's color
	- We need to redesign Game, to work without reconstruction of its instant for every move

## 2021-11-16
- We have begun to set up automated process to pass when next player has no moves.
	- considering combining Changeturn and Pass methods to automatically pass when change turn would lead to a no move situation.
	- can extend to check next two moves for two no move situations and leading to end game condition.

## 2021-11-30
- We worked on Game Over condition but there are still issues in the implementation
- As mentioned in line 72, we agreed the change could remove the complexity of the current design
- [X] Create an enumerable for game's status

## 2021-12-07
 - We improved SetStatus method
 - [-] Introduce a new class for SavedGame with board, turncolor, and status as properties
 - We need to do lots of cleanups
	- [x] (do line#87 first) Remove the obsolete Game constructor and update all affected unit tests
	- [x] Remove all duplicated methods that don't have color argument
	- [x] there must be room to make SetStatus method cleaner

## 2021-12-14
 - We performed code cleanup
 - We added ActOnStatus function to constructor but it is causing test issues as it is performing an action
 - [x] We should start next meeting by revisiting best practice for constructor to decide how to properly code this section

 ## 2022-01-04
 - Talked about responsibility for Core logic layer to act on game over status. 
	- one suggestion, to leave responsibility to UI, another suggestion to have interface class for output.
	- Static message handler classt to output messages to consol.
 - discussed moving to creating UI for application. Consol vs GUI. Technology used, Blazor vs Javascript.

## 2022-01-11
 - We started using Result as the return object
 - We started using Message to capture the activities behind PlaceCounter.
 - For the next session we are going to discuss further on what PlaceCounter should return. 
   One suggestion is to return Game, so the UI can access the latest properties of Game: like the turn, Status, and maybe the Message

## 2022-01-18
 - We reverted back to before result introduced
 - We decided to use flags to capture more than one status
 - Next session:
	[x] Refactor flags to separate class
	[x] Revisit what to do with status class
	- Mock up UI.

## 2022-01-25
- We added new Constants file for constants.
- Replaced Status flags with State class to track flags.
- Next week, start with mockup for console app.
- 

## 2022-02-01
- Create basic mock of console display.
- Created basic console app.
	[x] - Creating guides for board display.
	[x] - Handle and display states as required.

## 2022-02-08
- [X] Further refactoring of program. Possibly extracting methods to a new class.
- [X] play test full game, testing all states.
- [x] discuss next step, Web API/Blazor?

## 2022-02-15
- We ended in Green state
- We extracted the logic from Program to a new class, GameBoardUi
 [-] Handle bad inputs for UI

## 2022-03-01
- We watched half of a tutorial on Blazor to get an idea of how we can use it in Revesi: https://www.youtube.com/watch?v=MetcuX1OHD0
- We created a new MVC Core project (Reversi.Web) then created a simple Razor component to its home page.
- We think we can utilize some of the functionalities of Blazor for features such as hovering, placing counter on a squares, avoiding page refresh
- Next session we are going to start with a simple UI for Reversi and thinking about simple components we can develop for UI

## 2022-03-08
- Tested Blazor components parameters.
- [x] Create clickable grid of gameboard using blazor components.

## 2022-03-15
- Created new Grid Component to store all tiles (and a new Tile component)
- Created Grid for tiles, color coded it
	- Used Game.GetOutput() to get legal positions and found a bug that will need fixed in SetLegalPositions.
- [-] Fix GetOutput for first 4 moves of the game
- [x] Pass more information to tile component to allow for returning coordinates on button press

## 2022-03-22
- Added onclick functions to tile components
- Added row/col information to tile components
- [x] Find a way to pass tile coordinates to controller without page refresh (ideally)
- Possible solutions to look at next week are:
	- Update a hidden parameter in a form to be passed to a controller in some way (through json?).
	- Injecting a service to perform these actions
-[X] Need to revisit blazor Weather video https://www.youtube.com/watch?v=MetcuX1OHD0 to get better understanding of his service calls

## 2022-03-29
- We found the way to inject services to Razor components
- We started implementing GameService to eventually send the selected tile coordinates to PlaceCounter and get the updated Game intance back
- A possible solution to pass coordinates to the parent Razor component: https://www.pragimtech.com/blog/blazor/pass-data-from-child-to-parent-component-in-blazor/
- A possible way to pass the board to the service: https://beansoftware.com/asp.net-tutorials/managing-state-web-service.aspx
- Next session we will try to pass the coordinates to Game and return updated Game intance to Grid component

## 2022-04-05
- We used the service to track and update the game for the blazor components. 
- We also passed coordinates from child to parent component by using invoke event from https://www.pragimtech.com/blog/blazor/pass-data-from-child-to-parent-component-in-blazor/.
- We realized we did not need to bind manually, any time the component was updated the view updates on its own.
-[-] Need to find a way to initiate game inside controller instead of inside the service.
-[X] Set up more UI components (turn indicator, messaging)

## 2022-04-12
- We added two new components: Message and Game Information components
- [X] Need to find a way to initiate game inside Startup instead of inside the service.
- [X] Next session we will improve the new components
	- Changed Refresh mechanics of blazor components from @ref method to Cascading Value method where we cascade the service. 
- [X] Next session we will add scores to Game Information component

## 2022-04-19
- We found an alternative method for refreshing components, we randomized the start positions of the game.
- [-] Look into concern about new refresh method refreshing everything everytime someone clicks. Perhaps use a more targetted refresh.
- [X] Need to fix first 4 moves for start game that using false for randomization 
- [X] Discuss path forward
	- Android
	- 2 player in Blazor
	- AI (random or smart)

## 2022-04-26
- We fixed issues with placing first four tiles, and began refactoring within Game class.
- [X] refactor to do, update references to "position" to "square" instead.

## 2022-05-03
- We decided to look into 2 players
- We found this example which explains the similar scenario we decided to try (using SignalR and Blazor): https://www.c-sharpcorner.com/article/realtime-blazor-tic-tac-toe-game-bot-vs-multiplayer-using-signalr/
- We also found this example which is simpler and could be a better candidate to start with: https://docs.microsoft.com/en-us/aspnet/core/blazor/tutorials/signalr-blazor?view=aspnetcore-5.0&tabs=visual-studio&pivots=server
- [X] Next session we will start by implementing the Chat example (and we might keep it for future use)
- [X] Look for possible ways to decouple the logic from SignalR library (make it possible to work with other solutions)

## 2022-05-10
- We implemented chat in the game using signalR. (Note: We had an issue trying to use signalR 6 instead of 5)
- [X] Communicate via signalR that the board has changed after a move 
- [X] Add rules to restrict player moves to their own color's turn. 
	- [ ] Add messaging for when it is/is not your turn.
- [X] Limit to two players
- [ ] Possibly add chat to something to be persisted.

## 2022-05-17
- We started working on this item: "Add rules to restrict player moves to their own color's turn."
- We can review group in SignalR from the link Stephen shared: https://code-maze.com/how-to-send-client-specific-messages-using-signalr/
- [X] We can review StartUp and clean up any unncessary components (e.g. AddRazorPages)

## 2022-05-24
- We started associating connection IDs with player colors within the GameHub class.
- [X] Consider moving back to TDD to work out logic before implementing SignalR; i.e., in GameService / logic layer 
	  e.g., how to assign color to 1st player (black), 2nd player (white), etc.
- [ ] Handle disconnected connection IDs. Consider if we can leverage SignalR for this instead of our ConnectionList class.
- [X] ConnectionList class is static - consider another approach (maybe SignalR features or examples, e.g., DB).

## 2022-05-31
- We started implementing logic to manage users as they join the game.
- [ ] Revisit old logic for assigning color (black/white). 
- [X] Create a Player class (use class instead of char).

## 2022-06-07
- We added a player class to game
- [ ] Integrate hub/service to add/remove players from game

## 2022-06-14

- [X] Need to add connectionId to all place counter methods (currently optional)
- [X] Fix the "Invalid Move" bug in UI (maybe we need to call AddPlayer method)

## 2022-06-21

- We started working on calling AddPlayer from GridComponent
- We introduceed AddPlayer method when the hub is initialized in OnInitializedAsync
- [X] we need to review what is called in OnInitializedAsync and OnAfterRenderAsync. There are some redunant codes that we were not sure where they belong to.
- [X] Why do we get two connection IDs and two times AddPlayer invocation when we have only one client open
- [X] Joel: maybe we should comment out ChatHub
- [X] Add a logic to prevent adding connection Id for the player that is already added
- [X] ConnectionList in GameHub is not used. Maybe we should remove it.
- [ ] Explore the ways to automate test the hub with or without mocking.


## 2022-06-28
- We fixed the issue with assigning connection Ids to players as they join the game
- [X] Opening the third browser keep the game in a unfinished loop, we need to look into this
- [X] Show on browser what color is assigned to the player
- [X] Change the default new game constructor to initialize the initial placements (set to True as default)
- [X] reconsider this argument name: randomizeStartingMoves

## 2022-07-05
- [X] ConvertColourCharToString method; use it in grid component to see who the user is for that active browser.
- [X] Use a different error message when first player tries to make a move and second player has not joined yet.
- [ ] Add waiting for second player message when first player joins.

## 2022-07-12
- Discovered that OnAfterRenderAsync runs twice, so you need to use the firstRender parameter with a guard to avoid 
  duplicate behavior
- We learned that in SignalR our listener was defined as hubConnection.On<string>("AddPlayer",...) and we invoke it 
  with await hubConnection.SendAsync("AddPlayer");
- [ ] Visually improve display for whoAmI and whosTurn
- [X] Move player class to its own file
- [X] Go through old code for cleanup/refactor (ie: naming conventions,)

## 2022-07-19
- We did several refactoring and cleanups.
- [ ] Replace in all codes char Roles Constants with string ones and remove them from constant class 
- [ ] Line-by-line refactoring of classes other than Game and Constants
- [ ] Consider other ways to organize constants, e.g.:
```cs
		enum Counter { BLACK WHITE NONE }
		// toChar
		// toString

		abstract class Role

		class Player : Role // or enum?
			Counter counter 
			name string
  
		class Observer : Role

		// Browser: player 1, player 2, or observer


		====

		public Player Turn
```

## 2022-07-26
- We worked on error messaging, some refactor and tried to handle disconnecting from the game.
	- We left the disconnection problem in a state where after one person disconnects it appears to disconnect the wrong player.


