CC=g++ $(WARN) $(HEADERS) $(PLATFORM)
link=-lncurses
OBJECT_DIR=$(BUILD_DIR)/obj
BUILD_DIR=build
OUTPUT=$(BUILD_DIR)/zombie-hunter.exe
MKDIR=mkdir
RM=rm
WARN=-Wall
HEADERS=-I h
CODE_FOLDER=c/

ifeq ($(OS),Windows_NT)
  PLATFORM=-DWINDOWS
endif

_OBJ = main.o start.o engine.o avatar.o baddie.o terrainmap.o input.o display.o bullet.o
OBJ = $(patsubst %,$(OBJECT_DIR)/%,$(_OBJ))

zombie-hunter.exe: $(OBJ)
	$(CC) -o $(OUTPUT) $(OBJ) $(link)

$(OBJECT_DIR)/%.o: $(CODE_FOLDER)%.cpp buildfolder
	$(CC) -c -o $@ $<

buildfolder:
	$(MKDIR) -p $(BUILD_DIR)
	$(MKDIR) -p $(OBJECT_DIR)

clean:
	$(RM) -r $(BUILD_DIR)
