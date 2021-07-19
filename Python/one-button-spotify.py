import sys
##from gpiozero import Button
import spotipy
import spotipy.util as util
import time
import json

import RPi.GPIO as GPIO

ledPin = 11    # define ledPin
buttonPin = 12    # define buttonPin

# Modified from one-button-spotify: https://github.com/bishely/one-button-spotify
# Using freenove RPi.GPIO

username = 'USERNAME HERE'
password = 'PASSWORD HERE'
playlist = 'PLAYLIST URI HERE'
spotconnect_device_name = "DEVICE NAME HERE"

SP_CLIENT_ID = 'CLIENT ID HERE'
SP_CLIENT_SECRET = 'CLIENT SECRET HERE'
SP_REDIRECT_URI = 'REDIRECT URI HERE'

global token
global playing
global device

token = ''
playing = False
device = ''
scope = 'user-library-read, user-read-playback-state, user-modify-playback-state'

def setup():
    print 'setup called'
    
    GPIO.setmode(GPIO.BOARD)      # use PHYSICAL GPIO Numbering
    GPIO.setup(ledPin, GPIO.OUT)   # set ledPin to OUTPUT mode
    GPIO.setup(buttonPin, GPIO.IN, pull_up_down=GPIO.PUD_UP)    # set buttonPin to PULL UP INPUT mode

def spotStart():
    print 'spotStart called'
    global token
    token = util.prompt_for_user_token(username, scope,client_id=SP_CLIENT_ID,client_secret=SP_CLIENT_SECRET,redirect_uri=SP_REDIRECT_URI)
    print 'token created as ' + token

def spotDevices():
    print 'spotDevices called'
    global token
    try:
        global device
        sp = spotipy.Spotify(auth=token)
        devices = sp.devices()
        devices = devices['devices']
        dictionary = {}
        for item in devices:
            dictionary[item['name']] = item['id']
        device = dictionary[spotconnect_device_name]
        print 'device resolved as ' + device
    except:
        # empty token
        print 'something is not right - emptying token'
        token = ''
        spotStart()

def spotPlay():
    print 'spotPlay called'
    global token
    global playing
    try:
        if playing:
            print 'already playing - skip'
            # if we're already playing, skip to a new track
            sp = spotipy.Spotify(auth=token)
            sp.next_track()
            time.sleep(1)
        else:
            # if we're not playing, play the playlist, turn on shuffle and skip to a new (random) track
            print 'not playing - trying to start'
            sp = spotipy.Spotify(auth=token)
            sp.start_playback(device_id=device,context_uri=playlist)
            
            #print sp.current_playback()
            
            sp.shuffle(True)
            sp.next_track()
            playing = True
            time.sleep(1)
    except:
        # empty token
        print 'something is not right - emptying token'
        token = ''

def spotStop():
    print 'spotStop called'
    global token
    global playing
    try:
        if playing:
            # stop(pause) playing
            print 'currently playing - trying to stop'
            sp = spotipy.client.Spotify(auth=token)
            sp.pause_playback()
            playing = False
            time.sleep(1)
        else:
            # if not playing, do nothing
            print 'not playing - doing nothing'
            pass
    except:
        # empty token
        print 'something is not right - emptying token'
        token = ''

def loop():
    idle = 0
    global token
    while True:
        if not token:
            spotStart()
            spotDevices()
        if idle == 14400: # roughly every hour (14400*0.25secs = 60 mins) empty token
            idle = 0
            token = ''
        else:
            idle += 1
            time.sleep(0.25)
    
        if GPIO.input(buttonPin)==GPIO.LOW: # if button is pressed
            GPIO.output(ledPin,GPIO.HIGH)   # turn on led
            #print ('led turned on >>>')     # print information on terminal
            
            if playing:
                spotStop()
            else:
                spotPlay()
        else : # if button is relessed
            GPIO.output(ledPin,GPIO.LOW) # turn off led 
            #print ('led turned off <<<')    

def destroy():
    GPIO.output(ledPin, GPIO.LOW)     # turn off led 
    GPIO.cleanup()                    # Release GPIO resource

if __name__ == '__main__':     # Program entrance
    print ('Program is starting...')
    setup()
    try:
        loop()
    except KeyboardInterrupt:  # Press ctrl-c to end the program.
        destroy()
