VAR TimePeriod = ""
VAR userName = ""
VAR Date = ""
VAR Time = ""
VAR activity = ""
VAR dayOfWeek = ""

VAR rand = 2

VAR been1 = false
VAR been2 = false
VAR been3 = false
VAR been4 = false

->start_idle
=== start_idle === 
    ~been1 = false
    ~been2 = false
    ~been3 = false
    ~been4 = false
    ->reshuffle_idle
->DONE

=== reshuffle_idle ===
~rand =  RANDOM(1,4)
{rand == 1 && !been1: ->Idle_Chat.1_option | {rand == 1 : ->reshuffle_idle | }}
{rand == 2 && !been2: ->Idle_Chat.2_option | {rand == 2 : ->reshuffle_idle | }}
{rand == 3 && !been3: ->Idle_Chat.3_option | {rand == 3 : ->reshuffle_idle | }}
{rand == 4 && !been4: ->Idle_Chat.4_option | {rand == 4 : ->reshuffle_idle | }}
{been1 && been2 && been3 && been4 : ->Idle_Chat.5_option | }
->DONE

->Idle_Chat

=== Idle_Chat ===
    = 1_option 
        I see they refer to you as {userName}
        ~been1 = true
        ->reshuffle_idle
    = 2_option
        Hey {userName} Why are you not playin'?
        ~been2 = true
        ->reshuffle_idle
    = 3_option
        Actually it's {Time}, shouldn't you be {activity}
        ~been3 = true
        ->reshuffle_idle
    = 4_option
        This is the worst {dayOfWeek} I've had in months
        ~been4 = true
        ->reshuffle_idle
    = 5_option
        Don't you have anything better to do
        ->DONE
->DONE


    
->END