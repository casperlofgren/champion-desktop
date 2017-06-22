//  champion-format
//  Extracts useful data from champion.json

const config = require("./config.json");
const fs = require("fs");
const ncp = require("ncp").ncp;

// Path to ddragon data INCLUDING versionnumber (eg. ddragon\\7.12.1)
const ddragonpath = config.ddragonpath;

// ddragon path check
try {
    fs.statSync(ddragonpath + "/data/en_US");
}
catch (err) {
    if (err.code == "ENOENT") {
        console.log("ddragonpath is incorrect!");
    }
    else {
        console.log("Unhandled error occured while checking ddragon directory: " + err);
    }
    return;
}

// Read file with champion data
const champion = JSON.parse(fs.readFileSync(ddragonpath + "/data/en_US/champion.json"));

console.log(`Dumping data for version ${champion.version}\n`);

var champions = []; // Array for storing champion data

// Loop through all data objects
for (var i in champion.data) {
    // and add data to array
    champions.push({"name": champion.data[i].name, "id": champion.data[i].id, "key": champion.data[i].key, "title": champion.data[i].title, "image": champion.data[i].image.full});
}

// Make sure all champion icons exist
console.log(`Checking icons for ${champions.length} champions...`);

champions.forEach(function(champ) {
    fs.stat(ddragonpath + "/img/champion/" + champ.image, function(err, stat) {
        if (!err) {
            return;
        }
        else if (err.code == "ENOENT") {
            console.log(`Icon for champion ${champ.id} is missing!`);
        }
        else {
            console.log("Error while checking file: " + err.code);
        }
    });
});

// Copy data and write formatted data
try {
    fs.statSync("./data"); // Make sure directory exists
}
catch (err) {
    if (err.code == "ENOENT") {
        fs.mkdirSync("./data");
    }
    else {
        console.log("Unhandled error occured while checking ddragon directory: " + err);
        return;
    }
}

console.log("Copying icons...");
ncp(ddragonpath + "/img/champion", "./data/icons", function(err) { // Copy icons
    if (err) {
        return console.error(err);
    }
});

console.log("Writing custom champion data...");
fs.writeFileSync("./data/champs.json", JSON.stringify({"version": champion.version, "format": champion.format, "champions": champions})); // Write custom champion data


console.log("\nAll done!");
