﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawnbarrow
{
    internal class Game
    {
        public int currentRoomIndex;
        public (int x, int y) currRoomCoordinates;
        public string[] roomDescriptions =
        {
            //1 INDICES 0 ****1,1
            "In the brightest reach of this forest, You spot a silly little man, and it's evident that he has almost no business being here. He's small and frail, and has no definitive purpose in the land of Dawnbarrow. Whatever divine force led you to check the very bottom left corner of Dawnbarrow couldn't have prepared you for the site of this despicable, rather annoying man. Although you're in a rush to leave, this man tells you something that seems to haunt your very being. \n \"You must look for the fork in the road!\" ",
            //2 INDICES 1 ****2,1
            "Welcome to the jungle, a vast set of trees ranging from banana trees, to red cedars, to GIANT red cedars are littered throughout the area, there are all kinds of bugs as far as the eye can see. This is an awful place to be for people who hate humid temperatures. If I were you, I'd begin to wonder what creatures lurk here!",
            //3 INDICES 2 ****3,1
            "This is a weird spot of the jungle, there's a sense of danger to your north, but you can't help but feel the safest you've felt since you've begun this adventure, probably on account of the singing jungle birds soothing your soul, if ever you feel in trouble, or like you don't know what to do, this might be a good place to gather yourself when battle has made you weary.",
            //4 INDICES 3 ****4,1
            "Around you is a vast clearing, the open paths offer an expansive array of options, you can go in any cardinal direction, and find yourself in a new unknown enviornment",
            //5 INDICES 4 ****5,1
            "A humongous Hydra is on the coast of the Dawnbarrow sea. It's multiple heads are menacing, although there is one head that seems to be goofier than the others. It's impossible to gauge this creatures power, but I can assure you this Hydra is a force to be reckoned with!",
            //6 INDICES 5 ****1,2
            "\"Welcome to Dawnbarrow, there are a few things I wish to explain, at any given time, you can navigate in four cardinal directions: North, South, East, and West,\"A disembodied voice exclaims almost passive aggressively in a booming voice \"If for any reason you get lost or need help, don't hesitate to type the help command for more insight as to what you can do! Also refer to the location identifier in the top left hand corner of your screen for information about your surroundings, pay very close attention, as this identifier may disclose hidden information.\"  \nYou don't understand what the heck he is talking about, but you choose to just go with it. Why the creator of this game chose to nestle the tutorial into the very first room in his game world is beyond you. Anyhow, as you take in your surroundings, you notice that to the east is a heavy jungle, and to the north is more forest with plenty of trees. For some reason, the north path feels safer. \"The north path is safer for SURE!\" The disembodied voice screams one last time before his voice fades out of registry.",
            //7 INDICES 6 ****2,2
            "For the most part the jungle is a seriously dangerous place, there's not much worse than being caught out in the open with your pants around your ankles, a swarm of blowflies can be seen in a tiny clearing, There is also a shocking feeling of danger to the east. Anyone unprepared should feel anxious",
            //8 INDICES 7 ****3,2
            "Before you lies a gigantic Chimera, it's obviously guarding something rare, and the smell of this creature could peel paint off of a barn. The jungle is mostly quiet in this area, almost as if in respect for this behemoth. If you choose to do battle with this Chimera, be sure to proceed with caution, the corpses lying near promise one mean fight!",
            //9 INDICES 8 ****4,2
            "The Ocean beach is glistening with a prismarine color, surrounding you everywhere are silly creatures as far as the eye can see. There are starfish kissing the ground, and seagulls fighting over food. You're excited to be here, and the sun seems to mimic your excitement.",
            //10 INDICES 9 ****5,2
            "You take the beautiful path, it's obvious that you weren't cut out for the life of a hero, and even though you made it this far, some people just aren't apt for the savior lifestyle. The land around you is beautiful and stunning, but you certainly didn't do anything to deserve such a beautiful sight. Off in the distance you see a terrifying dragon breathing fire on a peaceful village, and although that is a terrifying sight, it's also gorgeous and not your problem. I hope you enjoyed taking the cowards way out, and that your beautiful scenery and your sissy serenity makes you happy. May you forever be blessed by the fortune of being a coward. THE END.",
            //11 INDICES 10 ****1,3
            "You walk into the forest, and it's evident to you that you're in the most peaceful area of DawnBarrow, the sunshine pokes through the trees beautifully and around you there are small critters that are minding their own business. Your attention fixates on a group of small rats that seem to appear endlessly almost out of thin air. It's worth mentioning that they aren't bothering anyone, certainly not you, but there is a looming feeling emanating from you that you should end their life without warning or reason...Should you choose to do so type \"fight rat\". Otherwise: to the east is a river, which seems to be the pointing in the direction of the centermost part of the land, and to the North is more forest. If you were feeling bold, you would probably take the path of the river, and if you were feeling like a sissy little crybaby, you would likely resume venturing north because grinding xp is necessary for longevity in these kinds of games I guess.",
            //12 INDICES 11 ****2,3
            "The river before you seems endless, and although you feel ready for anything, there's a looming feeling that the further you migrate east, the harder your journey will be. The sound of the gentle stream is in stark contrast with the smell of death and anguish lurking in the air, the two senses clash violently.",
            //13 INDICES 12 ****3,3
            "There's an almost urgent thought floating in your head. You believe it's your instinct that is telling you you're at the centermost part of the map, and that the entire land of Dawnbarrow is just a 5x5 grid.",
            //14 INDICES 13 ****4,3
            "The river roars ferociously, amped by the will of the surrounding River Kappas. This river, although beautiful seems to hold a mystery, there are stones all around in the running water, begging to be smashed into a million pieces",
            //15 INDICES 14 ****5,3
            "This is it, the fork in the road. Everything amounts to this. There are two paths: A southern path, which evokes friendly, positive vibes. The southern path is wonderfully well lit, and even gives off a scent of lilac and shea butter. The other path has a humongous door with a lock on it the size of your ego. The door looms with darkness and gives an evil and malicious feeling that would haunt even the bravest men. You have half a mind about you to enter, but you're not too sure about what to do with the lock.",
            //16 INDICES 15 ****1,4
            "As you make your way through the forest, you are entranced by wonderful atmosphere. As your feet crunch through sticks and rocks, you can't help but admire the wonderful acoustics in this forest. The debris being smooshed by your feet produces an almost rhythmic melody that pierces your ears with the sound of over one thousand sweet and beautiful lies whispered softly. The sound is reassuring, telling you that everything is going to be okay. Your body knows better. The chills sent down your spine are the answer to your prayers as you realize that rats aren't the only creature in this forest. Spiders, and tons of them are crawling out of every fibrous layer of twigs and branches. You prepare to run or fight.",
            //17 INDICES 16 ****2,4
            "The grasslands welcome you as you step forth into unfamiliar territory, Dawnbarrow holds such mysticism and beauty, and this spot specifically illustrates just that. There are a few low level monsters here, which are perfect for grinding, although you might want to equip a weapon first.",
            //18 INDICES 17 ****3,4
            "The grasslands hum a quiet tone as you take in your surroundings. It's darker here, and the smoke is only enveloping you more and more. Soon you can't see in front of you. You manage to gasp a breath of black fog, a feeling of danger is present. A Cerberus stands before you... and it's staring at you menacingly.",
            //19 INDICES 18 ****4,4
            "The cavernous den is lit only by a few sparks of static electricity brewing from the stalagmites and stalagtites in the room. You're surrounded by pitch black. You don't know what to do here, but it seems like there's something UP in here",
            //20 INDICES 19 ****5,4
            "Ahead lies a giant door, you need a key to open it, there's not much here to do without it, I suggest turning back if you don't have it, just make sure you don't head south at the signpost by accident!",
            //21 INDICES 20 ****1,5
            "In all of my years of narrating games, I have never seen such a wonderful forest, I am legitimately mesmerized once again, if only you could stop grinding xp for one second, you too could take in the beautiful scenery that is the Dawnbarrow forest, and you could see that the goblins that reside here aren't so bad once you've slain a few they've been known to hide things in areas like these, but I wouldn't pay it much mind. I just think that if you you could just see that there is a vast world out there, and that you can explore OTHER areas of Dawnbarrow, instead of camping out in the area that the developers intentionally made easier than the rest of the game.\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n                                                                                                                                                   \n I'll tell you what, why don't you go somewhere else for a change, I'll forgive you if you leave the forest.",
            //22 INDICES 21 ****2,5
            "Before you in the grasslands lies a wonderful sight. A cat! You have never seen a cat so beautiful, and never have you seen a cat this special, there are markings on his hind legs, and markings on his face too. These markings resemble patterns that might be found in a deck of playing cards. You are welcome to try and take him with you, but know there is a creature lurking in the tall grass who is ALSO eyeing your new bespeckled commpanion.",
            //23 INDICES 22 ****3,5
            "The grasslands seem to giggle as prarie dogs and other critters crack the surface of the ground, just to say hi. You're having fun watching the prarie dogs emerge from and then descend back into their burrows. This fun is broken by the sight of a slightly more aggressive prarie dog. You can't shake the feeling that there is more than one!",
            //24 INDICES 23 ****4,5
            "A group of miner goblins lie about in this cavernous den, it's a serious offense to exist as a human in the presence of a goblin, so they quietly stare at you as you move quietly through the expansive rocky terrain. Something that catches your eye is the presence of a giant pickaxe in the hands of one of the more bulky goblins",
            //25 INDICES 24 ****5,5
            "As you wander through the giant cave, you reach it's end, and before you lies a HUMONGOUS creature. It almost sounds as though he is crying, you could fight him, but maybe you could also do something else?"
        };
        public string[] roomsubtext =
        {
            //1 INDICES 0
            "Having just awoken, you find yourself in a strange meadow nestled in what seems to be a vibrant forest, with mild amounts of decaying architecture, certainly thousands of years old. There exists an exorbitant amount of small pests in the area. The smell of wildlife is all around you, but it's oddly comforting to be in such an area. There is a man standing near a sign, and a forest north of him. There seems to be nothing to the east, nothing to the west, and only a small rat in the southern direction. Your options are clear, you can head North, and engage with this stranger or you can head to the South, to investigate this rat.",
            //2 INDICES 1
            "The man before you seems happy to see you",
            //3 INDICES 2
            "You return to the strange meadow, and the man that was once here is gone, and the decaying architecture seems to want to be put out of it's misery",
            //4 INDICES 3
            "Around you is a vast clearing, the open paths offer an expansive array of options, you can go in any cardinal direction, and find yourself in a new unknown enviornment",
            //5 INDICES 4
            "nice",
            //6 INDICES 5
            "\"Welcome to Dawnbarrow, there are a few things I wish to explain, at any given time, you can navigate in four cardinal directions: North, South, East, and West,\"A disembodied voice exclaims almost passive aggressively in a booming voice \"If for any reason you get lost or need help, don't hesitate to type the help command for more insight as to what you can do! Also refer to the location identifier in the top left hand corner of your screen for information about your surroundings, pay very close attention, as this identifier may disclose hidden information.\"  \nYou don't understand what the heck he is talking about, but you choose to just go with it. Why the creator of this game chose to nestle the tutorial into the very first room in his game world is beyond you. Anyhow, as you take in your surroundings, you notice that to the east is a heavy jungle, and to the north is more forest with plenty of trees. For some reason, the north path feels safer. \"The north path is safer for SURE!\" The disembodied voice screams one last time before his voice fades out of registry.",
            //7 INDICES 6
            "wile",
            //8 INDICES 7
            "already",
            //9 INDICES 8
            "what the heck",
            //10 INDICES 9
            ":)",
            //11 INDICES 10
            "You walk into the forest, and it's evident to you that you're in the most peaceful area of DawnBarrow, the sunshine pokes through the trees beautifully and around you there are small critters that are minding their own business. Your attention fixates on a group of small rats that seem to appear endlessly almost out of thin air. It's worth mentioning that they aren't bothering anyone, certainly not you, but there is a looming feeling emanating from you that you should end their life without warning or reason...Should you choose to do so type \"fight rat\". Otherwise: to the east is a river, which seems to be the pointing in the direction of the centermost part of the land, and to the North is more forest. If you were feeling bold, you would probably take the path of the river, and if you were feeling like a sissy little crybaby, you would likely resume venturing north because grinding xp is necessary for longevity in these kinds of games I guess.",
            //12 INDICES 11
            "niceu",
            //13 INDICES 12
            "Having recently woken up, you have no idea about your surroundings, but you see this silly little man in the corner, eager to speak to you",
            //14 INDICES 13
            "this ought to be interesting",
            //15 INDICES 14
            "friends",
            //16 INDICES 15
            "what's up gangster",
            //17 INDICES 16
            "nice dude",
            //18 INDICES 17
            "what is this room",
            //19 INDICES 18
            "alrighty",
            //20 INDICES 19
            "friends,",
            //21 INDICES 20
            "gangganggang",
            //22 INDICES 21
            "howdy",
            //23 INDICES 22
            "boyohboy",
            //24 INDICES 23
            "friends",
            //25 INDICES 24
            "help me jafa"
        };
        //string idmeadow1 = "Having just awoken, you find yourself in a strange meadow nestled in what seems to be a vibrant forest, with mild amounts of decaying architecture, certainly thousands of years old. There exists an exorbitant amount of small pests in the area. The smell of wildlife is all around you, but it's oddly comforting to be in such an area. There is a man standing near a sign, and a forest north of him. There seems to be nothing to the east, nothing to the west, and only a small rat in the southern direction. Your options are clear, you can head North, and engage with this stranger or you can head to the South, to investigate this rat.";
        //string idmeadow2 = "The man before you seems happy to see you";
        //string idmeadow3 = "You return to the strange meadow, and the man that was once here is gone, and the decaying architecture seems to want to be put out of it's misery";

        //string idratfight = "You have encountered a rat! He look's kind of small though, I'm sure you can take him, realistically if you die here, you were never meant to play this game";
        //string idghoulfight = "You have encountered a Ghoul! Something tells me touching him will never get the smell out of your clothes.";
        //string idskeletonfight = "You have encountered a skeleton! There is legitimately nothing to be afraid of";
        //string iddragonfight = "You have encountered a ferocious dragon, his teeth are the size of your arms! It would take almost no effort for him to devour you whole";
        public void setCurrentRoom(int x, int y)
        {
            currRoomCoordinates = (x, y);
        }

        public void setcurrentRoomIndex(int x, int y)
        {
            //  currentRoomIndex = (x * y) - 1;
            // currentRoomIndex = (x - 1) + (y - 1) * 5;

        }
        public (int x, int y) getCurrentRoom()
        {
            return (currRoomCoordinates);
        }
        public int getcurrentRoomIndex()
        {
            return currentRoomIndex;
        }
        public bool checkRoom(bool Direction)
        {
            return Direction;
        }
        public string getMap()
        {
            int x = currRoomCoordinates.x;
            int y = currRoomCoordinates.y;
            string output = "";

            for (int col = 0; col < 5; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    if (row == y && col == x)
                    {
                        output += "X";
                    }
                    else
                        output += "O";
                }
                output = "\n";
                
            }
            return output;
        }
        public string Output()
        {
            int x = currRoomCoordinates.x;
            int y = currRoomCoordinates.y;

            int numColumns = 5;
            currentRoomIndex = (x - 1) + (y - 1) * numColumns;

            if ((currentRoomIndex >= 0) && (currentRoomIndex < roomDescriptions.Length))
            {
                return roomDescriptions[currentRoomIndex];
            }
            else return "You are in a strange, unmapped location, how did you get here?";
            
        }
        public string checkInput(string input, int currRoomx, int currRoomy)
        {

            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings"))
            {
                response = Output();
            }
            if ((input == "south") || (input == "South") || (input == "SOUTH") || (input == "s") || (input == "S")) 
            {
               response = "You start heading South"; 
            }
            if ((input == "north") || (input == "North") || (input == "NORTH") || (input == "n") || (input == "N"))
            {
                response = "You start heading North!";
            }
            if ((input == "east") || (input == "East") || (input == "EAST") || (input == "e") || (input == "E"))
            {
                response = "You start heading East";
            }
            if ((input == "west") || (input == "West") || (input == "WEST") || (input == "w") || (input == "W"))
            {
                response = "You start heading West";
            }

            return response;
        }
        public string checkInput(string input)
        {
            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings"))
            {
                response = roomDescriptions[currentRoomIndex];
            }
            if ((input == "south") || (input == "South") || (input == "SOUTH") || (input == "s") || (input == "S"))
            {
                response = "You start heading South";
            }
            if ((input == "north") || (input == "North") || (input == "NORTH") || (input == "n") || (input == "N"))
            {
                response = "You start heading North!";
            }
            if ((input == "east") || (input == "East") || (input == "EAST") || (input == "e") || (input == "E"))
            {
                response = "You start heading East";
            }
            if ((input == "west") || (input == "West") || (input == "WEST") || (input == "w") || (input == "W"))
            {
                response = "You start heading West";
            }
            if ((input == "fight") || (input == "kill" || (input == "murder") || (input == "")))
            {
                response = "You begin fighting";
            }

            return response;
        }

    }
}
