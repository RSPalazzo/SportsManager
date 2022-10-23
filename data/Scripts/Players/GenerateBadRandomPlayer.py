import random
import json

count = 751
total = 1000
while count <= total:  

    #First Names
    firstNameRand = random.randint(0,999)
    firstNameText = open("./data/Misc/FirstNames.txt", "r")
    firstNames = firstNameText.read()
    firstNameList = firstNames.split()
    #Last Names
    lastNameRand = random.randint(0,999)
    lastNameText = open("./data/Misc/LastNames.txt", "r")
    lastNames = lastNameText.read()
    lastNameList = lastNames.split()
    #Height
    heightRand = random.randint(0,16)
    heightText = open("./data/Misc/Heights.txt", "r")
    heightNames = heightText.read()
    heightList = heightNames.split()
    #Weight
    weightRand = random.randint(150,300)
    #Handedness
    handednessRand = random.randint(0,9)
    if handednessRand == 9:
        handedness = 'Left'
    else:
        handedness = 'Right'
    #Personality
    personalityRand = random.randint(0,4)
    personalityText = open("./data/Misc/Personalities.txt", "r")
    personalityNames = personalityText.read()
    personalityList = personalityNames.split()
    #Physical
    agilityRand = random.randint(1,6)
    flexibilityRand = random.randint(1,6)
    balanceRand = random.randint(1,6)
    staminaRand = random.randint(1,6)
    strengthRand = random.randint(1,6)
    #PlayerCondition
    playerCondition = 50
    #Mental
    awarenessRand = random.randint(1,6)
    determinationRand = random.randint(1,6)
    positivityRand = random.randint(1,6)
    demeanorRand = random.randint(1,6)
    fortitudeRand = random.randint(1,6)
    #Equipment
    fitRand = random.randint(1,6)
    qualityRand = random.randint(1,12)
    equipAccuracyRand = random.randint(1,12)
    #Mechanics
    ballStrikingRand = random.randint(1,6)
    accuracyRand = random.randint(1,6)
    swingRand = random.randint(1,6)
    tempoRand = random.randint(1,6)
    shotShapingRand = random.randint(1,6)

    data = {}
    data['playerFirstName'] = firstNameList[firstNameRand]
    data['playerLastName'] = lastNameList[lastNameRand]
    data['playerFullName'] = firstNameList[firstNameRand] + ' ' + lastNameList[lastNameRand]
    data['playerHeight'] = heightList[heightRand]
    data['playerWeight'] = weightRand
    data['playerImage'] = 'null'
    data['playerCountryImage'] = 'null'
    data['playerHandedness'] = handedness
    data['playerPersonality'] = personalityList[personalityRand]
    data['attributes'] = {}
    data['attributes']['physical'] = {}
    data['attributes']['physical']['agility'] = agilityRand
    data['attributes']['physical']['flexibility'] = flexibilityRand
    data['attributes']['physical']['balance'] = balanceRand
    data['attributes']['physical']['stamina'] = staminaRand
    data['attributes']['physical']['strength'] = strengthRand
    data['attributes']['playerCondition'] = playerCondition
    data['attributes']['mental'] = {}
    data['attributes']['mental']['awareness'] = awarenessRand
    data['attributes']['mental']['determination'] = determinationRand
    data['attributes']['mental']['positivity'] = positivityRand
    data['attributes']['mental']['demeanor'] = demeanorRand
    data['attributes']['mental']['fortitude'] = fortitudeRand
    data['attributes']['equipment'] = {}
    data['attributes']['equipment']['fit'] = fitRand
    data['attributes']['equipment']['quality'] = qualityRand
    data['attributes']['equipment']['accuracy'] = equipAccuracyRand
    data['attributes']['mechanics'] = {}
    data['attributes']['mechanics']['ballStriking'] = ballStrikingRand
    data['attributes']['mechanics']['accuracy'] = accuracyRand
    data['attributes']['mechanics']['swing'] = swingRand
    data['attributes']['mechanics']['tempo'] = tempoRand
    data['attributes']['mechanics']['shotShaping'] = shotShapingRand

    json_data = json.dumps(data)
    with open('./data/Players/Player'+ str(count) + '.json', 'w') as outfile:
        outfile.write(json_data)
    count += 1