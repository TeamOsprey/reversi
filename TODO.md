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
- [X] Need to revisit blazor Weather video https://www.youtube.com/watch?v=MetcuX1OHD0 to get better understanding of his service calls

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
- [-] Need to find a way to initiate game inside controller instead of inside the service.
- [X] Set up more UI components (turn indicator, messaging)

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
	- [x] Add messaging for when it is/is not your turn.
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
- [X] Handle disconnected connection IDs. Consider if we can leverage SignalR for this instead of our ConnectionList class.
- [X] ConnectionList class is static - consider another approach (maybe SignalR features or examples, e.g., DB).

## 2022-05-31
- We started implementing logic to manage users as they join the game.
- [X] Revisit old logic for assigning color (black/white). 
- [X] Create a Player class (use class instead of char).

## 2022-06-07
- We added a player class to game
- [X] Integrate hub/service to add/remove players from game

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
- [X] Visually improve display for whoAmI and whosTurn
- [X] Move player class to its own file
- [X] Go through old code for cleanup/refactor (ie: naming conventions,)

## 2022-07-19
- We did several refactoring and cleanups.
- [X] Replace in all codes char Roles Constants with string ones and remove them from constant class 
- [ ] Line-by-line refactoring of classes other than Game and Constants
- [X] Consider other ways to organize constants

## 2022-07-26
- We worked on error messaging, some refactor and tried to handle disconnecting from the game.
	- We left the disconnection problem in a state where after one person disconnects it appears to disconnect the wrong player.


## 2022-08-02
- [ ] When one player disconnects notify the other player and end the game.
- [ ] Have multiple rooms with one game per room. Note: This means we might not have an observer any more; e.g., a third connection will be in a new room.

## 2022-08-09
- Replaced lingering hardcoded characters and strings with related constants.
- [X] Worked on changing roles from a constant to an enum but left as WIP with some errors. IN PROGRESS
- [X] Change turn to Player class in Game class (review all methods where we are passing RoleEnum and discuss where player object would be more beneficial)
	  - Note: We have 1. RoleEnum, 2. Counters (char constants), 3. Player class
	  - Maybe remove Observer from RoleEnum and rename as PlayerEnum (or equivalent)

## 2022-08-16
- Investigated the remove player issue, and decided we need to investigate disposeasync of hubconnection.
- Finished implementing RoleEnum and removed Observer from the class.
[ ] Review ReSharper code inspection results (esp. warnings)
[ ] Consolidate RoleEnum/Counters/Player Class
[ ] Review method names and parameters for clarity of meaning?

## 2022-08-23
- We are in RED state after introducing Counter to Player
- We need to find a new way to initiate the first 4 counters on board:
	1- Make a new method to place the first 4 counters independent from players OR
	2- Wait for 2 players to join the game before initiating the board with 4 counters OR
	3- Create players independent from connection
- We might want to change the data type of _turn and _opponent fields to Player

## 2022-08-30
- We didn't code at all!
- We decided to use the visual method of Event Storming to discuss the process. Here is the link of the board we created: https://miro.com/app/board/uXjVPbHT2Ag=/?share_link_id=207523459816
- We decided to go with method 1, form previous session, to initiate the board
- We also decided to create an array of all possible combinations of first four moves and choose from it randomly

## 2022-09-06
- Ending in RED state
- discuss changes made on 08-23 which put us in RED state.
- We changed construction of Board to start with one of 6 initial board states.
- Examine possible root cause of accessing a Player who does not yet exist. (see todo on line 273 in Game.cs)

## 2022-09-13
- We are back to GREEN
- However, we encountered a new issue when tried to run the game with two players. When the second player connects we get this error: "Collection was modified; enumeration operation may not execute."
- The error happened on line 119 of Game.cs
- Mark's suggestion is to remove the second Any
- Joel's suggestion is to revisit the gate keeper we added to line 76
- Also add an additional condition at line 124 to avoid setting status if it is already initiated
- Add a helper method to Unit Tests to avoid duplication codes for adding two players.

## 2022-09-20
- We fixed a race condition in the AddPlayer method, in which the playerList collection was being accessed and 
  modified at the same time. We resolved this with a lock statement.
- We encapsulated the PlayerList into a reusable collection object.
- [ ] IMPORTANT!!! : "Who am I" is showing White for both <=====*************** !!!!!!!!!
- [ ] Revisit the Lock we added to AddPlayer method. We might want to change its scope.
- [X] Encapsulate PlayerList to its reusable collection object.
- [ ] Refactor PlayerList class if needed.

## 2022-09-27
- [ ] WIP: Reviewing and fixing some ReSharper errors/warnings
- [ ] WIP: Refactored some methods into PlayerList class
- [ ] Consider adding EN-CA dictionary to ReSharper (see https://github.com/wooorm/dictionaries/tree/main/dictionaries/en-CA)
- [ ] Refactor Linq statements from Game to PlayerList class(approx 4 left.)

## 2022-10-04
- Investigated Who am I issue when duplicating tabs. Did not solve. Seems to have to do with a possible race condition on checking the colour. 3rd connectionId also shows up.
- [ ] WIP: Need to review game after changes made to playerList object/logic to start with 2 players and only change the connectionId from null.

## 2022-10-11
- We are going to try the following possible solutions for 3rd connectionId issue:
	- Go back to earlier branch to see code when it was working
	- A new blazor project with focus on connectionId, a simple tracking of connectionId (singlton counter to show the number on browser: 1, 2, 3, ...)
	- Reading more documents or stackoverflow posts to find possible other people with similar problem
	- Add an automated test to check blazor behaviour for this and other situations	
- We fixed the Who Am I white/black issue by reverting GridComponent.razor to the previous version, which used hubConnection.ConnectionId in OnInitializedAsync() instead of the connectionId 
  parameter in the hubConnection.On blocks, but there are still some issues, such as:
  - [ ] When you refresh one of the browsers, the Who Am I switches (i.e., the white and black players swap browsers). This is true even when one player is in incognito Chrome 
	    and the other in regular Firefox. Try looking at file history of GridComponent.razor. Maybe an older version didn't have this problem.
- [ ] Only show "Invalid Move" on the browser of the player who made the invalid move. Currently, this is a problem only after manually refreshing a browser.
      Something like a state variable might solve the problem. (Look into session/global vs. user/connection variables)
- [ ] Investigate the difference between hubConnection.ConnectionId vs the connectionId parameter in the hubConnection.On blocks.
- We found a good shortcut: To Find in Files, use Ctrl-Shift-F
	
## 2022-10-18
- We tried adding the following lines to appsettings to better trace the SignalR connections:
   - "Microsoft.AspNetCore.SignalR": "Debug",
   - "Microsoft.AspNetCore.Http.Connections": "Debug"
   - One of the unexpected observations we had that for one open browser 3 connection Ids were generated and when refreshed the browser, 3 new ones generated but the ID of the ones removed were different from the original connection Ids
- [ ] Mark found this article that suggests not to use connection id to track users: https://consultwithgriff.com/signalr-connection-ids/
	- We need to dig into two options to figure out which one is more suitable for us: using Groups or Users
	- For User (HubConnectionContext.User) we might run into an issue for testing multiple users by opening a new tab. This might not work as expected.
	- We can read more about the above options here: https://learn.microsoft.com/en-us/aspnet/core/signalr/groups?view=aspnetcore-6.0
	- We might want to look for a sample project where connection Id was not used to track users.
	- What's the possible use case for connection Ids under HubConnectionContext?

## 2022-10-25

- We worked on switcing from connectionId to userId. In GameHub we were able to access userId but we couldn't find out how to access it in GridComponent.
- We tried changing how HubConnection is created in GridComponent, but we got stuck on how to define AccessTokenProvider to the program.
- We replaced all connectionIds in GridComponent, we will need to debug it next session to see what is the value of userId when AddPlayer listener is called.

## 2022-11-01

- Worked on tracking Userids of players.
- Using Cookies to track the userId of each player.
- see: https://stackoverflow.com/questions/69171418/append-cookie-signalr-core
- Currently when updating cookie for new player, we get an error about changing the respose after it has been created.
- [ ] Create middleware to add key to cookie for player if it does not already exist.
- [X] UserId property getter requires much refactoring to remove hardcoding.
- [ ] Look into TryGetValue for cookie to lower number of accesses to it.
- [ ] Look through tasks and parameters in Gamehub to find and remove obsolete code.
- [ ] see issues associated with line 357.

## 2022-11-08
- We managed to get Who I am working again
- We moved cookie setting from GameHub to GridComponent (since it is called first) by injecting HttpContextAccessor (@inject IHttpContextAccessor HttpContextAccessor).
- We realized that the listener was expecting a string input argument. We made it work by adding a test string to the 
tasks but we need to look into a way to make it work without any input argument.
	- Perhaps by checking the overload of hubConnection.On.

## 2022-11-15
- [ ] See if we can remove this unused string parameter (hack): private const string UnusedParameterToGetAroundRuntimeErrorMystery = "foo";
   - [X] remove from AddPlayer
   - [ ] remove from RemovePlayer (this method is not used in GameHub and GridComponnent and should be deleted)
	- We can use ReceiveUpdate as a template for fixing this but when tried something unexpected happened and we had no time left to fix so we reverted the attempt.
- [X] Currently first refresh does not work. Need to fix this. This should refresh 1st players screen after 2nd player is added.

## 2022-11-22
- [ ] Find an alternative method to assign an id to users. Maybe a GUID or a sequential number or a combination of it with date-time.
- We noticed unlike AddPlayer and RemovePlayer tasks, the name of task to invoke ReceiveUpdate is different: SendUpdate
- ReceiveUpdate is now called RefreshUI.
- Shall we replace the strings for listeners and tasks (in GameHub or GridComponent,...) with constants or nameof/reflection 

## 2022-11-29
- [X] We need to find out why refresh is intemittent. When the white player joins the black player's board doesn't refresh always.
- [X] During our test we noticed once the player was set to Observer in one of the new browsers (maybe after the second browser was closed and opened a new one). 
Why we are still able to see a player as an Observer?
- [ ] Our first attempt to remove RemovePlayer from GameHub and GridComponent seemed to made refresh problem more frequent. We reverted it but we need to give it a try again.
- [ ] We could replace line 34 of GameHub (await Clients.All.SendAsync("AddPlayer"); ) with the direct call to AddPlayerTask.

## 2022-12-06
- We fixed intermittend refresh issue
- We fixed issue with Observer
- We agreed to look into hosting the game on a cloud server (perhaps Azure)
- [ ] Revisit the name of tasks to communicate they are notifying all clients instead of adding a player for instance
	- Or shall we inject the service to the hub?

## 2022-12-13
- Published Reversi to Azure Web Services.
- Now getting Null exception when attempting to access cookies.
- There may be issues with accessing HTTPContext within Blazor pages.
	- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-6.0
	- https://learn.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-7.0
- considering reading the cookie on page load, and passing the cookie value to the Blazor components through a variable, or a session variable.
- Stephen will continue sharing access to the Reversi app within Azure.

## 2022-12-20
- Successfully got reversi working on reversi.Azurewebsites.net
- Moved cookie access from blazor component to MVC index page
- [X] Look at refactor of grid component and other UserId uses(UserId cleanup, formatting)
- [X] Only show green tiles to user whose turn it is, see also Todo item about invalid move messages.
- [ ] Add end of game UI 
- [ ] Revisit old todo.

## 2023-01-03
- [ ] Shall we move listeners' logic from GridComponent to GameHub? We can ask from Stephen if that was what he meant.
- We have started making changes in Game.cs around line82 GetCurrentState() to remove green squares from display of inactive player (see line 443 ToDo).
		- Considering redesign of Game to support multiple clients instead of only stand alone application.
		- We decided to leave this as is and handle the display in the UI layer (hide legal moves from inactive user)
- [ ] refactor Game.cs within GetOutput method confusing naming of methods. Noted with a ToDo within file.
		-"ReversiBoard.SetLegalSquares(GetLegalSquares(_turn)); // todo: consider renaming"

## 2023-01-10
- We now show the InvalidMove message and legal squares (in green) to the current player
- We used ChatGPT to see if it could take turns using TDD (using one test case for fizzbuzz as an example) - and it worked (at least with a short test)
- [x] Don't show InsufficientPlayers message after we have both players
- [x] Refactor GetMessage and GetPersonalMessage to remove redundant code

## 2023-01-17
- [x] Fix failing test: IfPlayersMoveIsOverNextPlayerTurnsPassAndFlagDeclarePass
- [x] We commented out a "new State()" in Game.SetStatus to fix a failing unit test. Now we need to check its impact on UI (valid moves and messages).
- [ ] Consider if further refactoring for GetMessage and GetPersonalMessage 

## 2023-01-24
- [ ] We need to refactor ConfirmTwoPlayers method. We are getting the same information from different places. 
The method also sets state which doesn't seem to be the right responsibility of this method.
- [x] Consider refactoring State class.
- [x] We can watch this video next week and try to apply its practice to our code: https://www.youtube.com/watch?v=YtROlyWWhV0

## 2023-01-31
- We watched the first 15 minutes of the video mentioned last week (https://www.youtube.com/watch?v=YtROlyWWhV0),
  which suggested refactoring code with a lot of branching (e.g., if-thens) by using an object model with factories,
  so the branching would be inside the factory instead of all over the code. We identified the State class as a 
  good candidate for this refactoring, and started working on it.
- [x] Remove unused State flags, TurnComplete and InProgress. And remove tests associated with these flags.
	  (Note: The 4 remaining states are mutually exclusive.)
- [x] Refactor State class to be an abstract class, and derive 4 classes for the 4 different states. 
	  - See commented out code that we started in State.cs. 
	  - When creating the State objects, use a factory.
	  - Note: After the meeting Joel was wondering if it would be easier to convert the State to an enum first,
	  and then use the enum in the factory to create the correct State object. Also, Game.cs doesn't use the
	  IsPersonal or ErrorMessage attributes of the state, so it doesn't need to know about them, only about the enum; 
	  So maybe the factory could/should be in the UI (or possibly in the GameService).

## 2023-02-07
- Refactored State into subclasses
- [ ] Do we want to get rid of the old text-based UI (GameBoardUi class)?
- [x] Let players know if it's their turn or their opponent's turn. (see line 211 in this file (todo.md))
- [ ] Improve text at top of UI: 
	- Move score below game board and format better (e.g., bigger text, etc.)
- [ ] Consider moving game board into its own component (razor file)

## 2023-02-14
Refactoring changes to Player class.
- [x] Replace PlayerType enum with subclassing
- [x] Remove PlayerType Enum (Roles.cs file)

## 2023-02-21
We finished replacing PlayerType enum with subclassing, but still need some clean up. We are green and UI works.
- [X] Do more cleanup in Game.cs and PlayerList.cs - refactoring, consider ideas from video (if-thens/responsibility of subclasses, etc.)

## 2023-03-07
Refactored Game.cs and PlayerList.cs:
- Renamed PlayerList class and instance to PlayerCollection and _players. See .NET Guidelines for collections: 
	- "Use the "Collection" suffix in names of types implementing IEnumerable (or any of its descendants) and representing a list of items."
	- Source: https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/guidelines-for-collections
- Renamed PlayerList.IsGameFull to .HasAllPlayers
- Add and Remove player are now more encapsulated in PlayerList
- [x] Fix this in PlayerCollection class: "DO NOT use ArrayList or List<T> in public APIs." (from .NET Guidelines for collections)
	- don't expose the list of players property that is inside the PlayerCollection
- [X] Consider refactoring the Add Player functionality further (in Game.cs and PlayerCollection.cs): 
	  Change PlayerCollection.AddPlayer to .TryAddPlayer and return a bool indicating if the player was added or not.
	  This would allow us to:
	  -[X]  Delete the field _setInitialStatus (and check instead of using that field, just check the result of TryAddPlayer).
	  -[X] Move "if (_players.HasAllPlayers || _players.Contains(userId)) return;" into TryAddPlayer and return false in that case.
- Tip: Install Microsoft Power Toys and use Win+Shift+T to get OCR on rectangle of screen. (We used this to get all the checkin comments 
  and paste into this todo document.) Links: 
	- Power Toys overview: https://learn.microsoft.com/en-us/windows/powertoys/
	- Paste as Plain Text (OCR): https://learn.microsoft.com/en-us/windows/powertoys/paste-as-plain-text

## 2023-03-14 
- Updated TargetFramework to net6.0 where not yet done	
- We continued refactoring and encapsulating PlayerCollection

## 2023-03-28
- Refactored AddPlayer: removed redundant check from Game.AddPlayer and moved existing userId check to PlayerCollection class.
- WIP: Run Extensions > ReSharper > Inspect > Code Issues in Solution
	- [ ] Review all warnings and refactor as appropriate
- [ ] Docker image in GitHub (CI/CD) (discuss if desired)

## 2023-04-04
- Created new branch for updating creation of initial board. (Branch: RefactorCreateInitialBoard)
- [x] Refactor Board's CreateInitialBoard method to something more compact (algorithm)
		- Restored RandomlyPlaceInitialCounters method to Board.cs, next session we will discuss how to proceed with either refactoring this method or choosing a different path.
			- Possible solution: https://stackoverflow.com/questions/19201489/using-linq-to-shuffle-a-deck 
			- See also: https://dotnetfiddle.net/4vs69H

## 2023-04-18
- We finished refactoring the createInitialBoard
- [ ] consider the way we access co-ordinates of the boad in a class?
  - a board has a class with cells, with method, 'GetMiddleCells'
  - consider a BoardFactory in case it helps
  - discover areas where the 'Game' is doing task which are in the domain of the 'Board' (Seperation of responsibility)
- [ ] Consider the 'Game' engine, and how it re-initialize when people want to play again. (Do we re-randomize the board?)

## 2023-04-25
- We removed the code not needed with the current initial squares business rules
- We refactored Game and Board and changed some names to make them more understandable
- [x] IsAdjacentSquareValid is both validating and adding to legal squares. We will need to fix it so it doesn't do more than one thing.
- [ ] Consider clearning two models of representing the board. One as a list of strings and other is two dimensional array of squares.
- [ ] See also additional board refactoring above (line 542+)

## 2023-05-02
- We continued refactoring the board
- [X] Game.WouldMoveCauseCaptureInGivenDirection duplicates code. This method currently just called from GetLegalSquares, but we do 
      something similar for when we are actually capturing squares. (We need to find this other code.) Also, consider more 
	  renaming/refactoring of GetLegalSquares and WouldMoveCauseCaptureInGivenDirection.

## 2023-05-09
- We worked on refactoring GetCapturableSquares and GetLegalSquares
- [X] Think about refactoring AllInitialTilesPlaced() method and move it to Board.cs instead of Game.cs
- [X] GetCapturableSquares and GetLegalSquares do essentially the same thing; They build lines. 
      Can we refactor out the 'line builder' from these two. (Place the loop in a single function that is called from both.)
	  Optionally - Refactor out the loop that goes in all directions. This might require passing in a deligate, or using a factory/polymorphism.

## 2023-05-16
- Deduplicated GetLegalSquares logic. 
- [x] Create a property for Game to hold the LegalSquares Dictionary for white and black

## 2023-05-23
- Refactored legal squares further and unhardcoded redundant board size constants
- [X] Refactor redundant code Turn() and GetCurrentPlayer()
- [ ] If possible, encapsulate _whiteLegalSquareDictionary and _blackLegalSquareDictionary into a property (with getter/setter).
	- [ ] Consider redundancy of always making 2 calls to set the 2 dictionaries (instead of 1 call)