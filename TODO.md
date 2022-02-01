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
- [ ] Develop the Consol app to play by sending commands


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
	[] - Creating guides for board display.
	[] - Handle and display states as required.
