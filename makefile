CC=g++ $(WARN) $(HEADERS) $(PLATFORM)
link=-lncurses
DEBUG=-g
OBJECT_DIR=$(BUILD_DIR)/obj
BUILD_DIR=build
MKDIR=mkdir
RM=rm
WARN=-Wall
HEADERS=-Ih -I$(MINGWINCLUDE)\ncurses
CODE_FOLDER=c/
MINGWINCLUDE=C:\MinGW\include


ifeq ($(OS),Windows_NT)
  PLATFORM=-DWINDOWS
endif

_OBJ = main.o start.o engine.o terrainobject.o avatar.o baddie.o terrainmap.o input.o display.o bullet.o
DEBUG_OBJ = $(patsubst %,$(OBJECT_DIR)/debug/%,$(_OBJ))
RELEASE_OBJ = $(patsubst %,$(OBJECT_DIR)/release/%,$(_OBJ))

build-debug: $(DEBUG_OBJ)
	$(CC) $(DEBUG) -o $(BUILD_DIR)/debug/zombie-hunter.exe $(DEBUG_OBJ) $(link)

$(OBJECT_DIR)/debug/%.o: $(CODE_FOLDER)%.cpp buildfolder-debug
		$(CC) $(DEBUG) -c -o $@ $<

build-release: $(RELEASE_OBJ)
	$(CC) -o $(BUILD_DIR)/release/zombie-hunter.exe $(RELEASE_OBJ) $(link)

$(OBJECT_DIR)/release/%.o: $(CODE_FOLDER)%.cpp buildfolder-release
	$(CC) -c -o $@ $<

buildfolder:
	$(MKDIR) -p $(BUILD_DIR)
	$(MKDIR) -p $(OBJECT_DIR)

buildfolder-debug: buildfolder
	$(MKDIR) -p $(BUILD_DIR)/debug
	$(MKDIR) -p $(OBJECT_DIR)/debug

buildfolder-release: buildfolder
		$(MKDIR) -p $(BUILD_DIR)/release
		$(MKDIR) -p $(OBJECT_DIR)/release

clean:
	$(RM) -r $(BUILD_DIR)
