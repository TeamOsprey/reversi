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
- [ ] Add the logic to check for first 4 legal moves: 
		* if the board is empty, only one counter can be placed in one of the legal 4 central squares
		* if there is aone counter in the middle 4 squares, the opponent can place in 3 remaining middle squares
		* continue until all 4 counters are placed
- [ ] Recognize the PASS turn
- [ ] Develop the Consol app to play by sending commands


## 2021-11-02
- We started working on Pass rule and finished in GREEN state!
- Next week we will need to start with checking the rules about when and how to Pass
	- Shall we only inform the player or give option to initiate thePass?

## 2021-11-09
- We agreed that setting the status of Game doesn't belong to the class' constructor
- We talked about some redesign before we continue
	- We may need to set the status of Game right inside the PlaceCounter method. So we won't wait for the next player to make the move before we find out what status is.
	- We will probably need a method to give us the legal positions by providing it the player's color
	- We need to redesign Game, to work without reconstruction of its instant for every move