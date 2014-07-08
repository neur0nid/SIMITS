SIMITS
======

Communication simulator in the field of ITS to measure MAC performance.

This software is a communications simulator in the field of the Intelligent Transportation Systems (ITS). It is able to simulate and to measure the performance of different medium access control mechanisms (MAC), including the following:
- Slotted-aloha
- RR-Aloha
- ENCCMA (author's proposal)

The simulation experiments can be performed considering a convoy of autonomous vehicles which follow a trajectory on a limited area. To guarantee the convoy stability, certain messages must be interchanged among the vehicles members of
the convoy. In this point, the behaviour of different MAC proposal are measured in terms of throughput as each vehicle must be able to access the shared medium to achieve a successful transmission. To do that, the medium access control
mechanism have to cope with collisions among vehicles and with external interferences.

The software allows to modify several parameters as the number of vehicles, type of MAC, the way to divide the shared medium in different combinations of TDMA and FDMA, and the presence of interferences.

It also presents the evolution and the results of the experiment in screen and in *.csv files.

The development of this tool is related to the following technical papers:

"Medium Access Control Based on a Non Cooperative Cognitive Radio for Platooning
Communications"
Manzano, M.; Espinosa, F.; Bravo, A.M.; Garcia, D.; Gardel, A.; Bravo, I.
Intelligent Vehicles Symposium, 2012 IEEE. Pag.: 408 - 413. 
Doi: 10.1109/IVS.2012.6232172
http://ieeexplore.ieee.org/xpl/articleDetails.jsp?tp=&arnumber=6232172&searchWithin%3Dp_Authors%3A.QT.Manzano%2C+M..QT.%26searchWithin%3Dp_Author_Ids%3A37838071800

“Dynamic Cognitive Self-Organized TDMA for Medium Access Control in Real-Time 
Vehicle to Vehicle Communications”
Mario Manzano, Felipe Espinosa, Ángel M. Bravo-Santos, Enrique Santiso, Ignacio 
Bravo, and David García,
Mathematical Problems in Engineering, vol. 2013, Article ID 574528, 13 pages, 
2013. doi:10.1155/2013/574528
http://www.hindawi.com/journals/mpe/2013/574528/

Contact: espinosa@depeca.uah.es, mariomanzanovazquez@gmail.com

This software have been written in C#.



2014

