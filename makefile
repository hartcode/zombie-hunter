CC=g++ $(WARN) $(HEADERS) $(PLATFORM)
link=
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

_OBJ = avatar.o terrainmap.o input.o display.o engine.o main.o
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
