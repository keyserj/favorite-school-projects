## Purpose
This repo isn't for keeping projects up-to-date; it's for showing off the projects I most enjoyed working on in college. Two projects didn't even have coding involved, and the one project that did is nothing to brag about with regards to code quality.

## Sliding Puzzle Solver
I worked on this project's back-end with one teammate, and the Unity front-end was done by a third teammate. The program solves any solvable 3x3 sliding puzzle using any of three algorithms.

My favorite part about this project was being able to effectively debug somewhat-complicated algorithms through pair programming, and in the process of doing so, we first-hand experienced the value of using the right data structures. Our biggest performance increase went from solving a 31-step (that's the highest # steps) sliding puzzle in 15 minutes to solving it in several seconds, merely by changing a couple Lists into HashSets (we only needed to use those collections for checking if they contained specific values). A smaller chunk of time was shaved by changing some SortedLists to PriorityQueues (the collections of puzzle states were accessed in order of least expected # steps to solve, and we were using a jank implementation of SortedList to allow duplicate keys). We were also given some freedom in choosing the three algorithms we wanted to use for traversing the state space; we ended up using Breadth-First, A*, and Greedy-Best-First algorithms.

## The Fundamentals of Reinforcement Learning
All Software Engineering majors at UW-Platteville are required to take a Senior Seminar course. Each student gets approval on a topic to research, then writes a paper and gives a presentation on that topic.

At the time I was choosing, AI piqued my curiosity; I only knew some incredible accomplishments that were achieved through its use, but it was magic to me. So I did a few Google searches, looking into specific AI techniques and algorithms, and I found Reinforcement Learning to be the most interesting. It's the concept of having the AI learn closely to how a human learns through positive and negative reinforcement. I really liked the assignment because it was entirely a quenching of my curiosity along with a formal attempt to explain my findings, which helped me concretize ideas that would otherwise have been left more abstract and less understood.

## Software Development Anti-Patterns
I always try to understand best practices in the world of software in hopes of developing quicker and with higher quality. But to the same end from an opposite perspective, I can try to understand bad practices to avoid developing slower and with lower quality!

This project was for my Capstone course (these presentations were given during class, while working on the Capstone project outside of class). I worked on and presented it with a partner, and we were given the freedom to choose this topic.

It was tons of fun because, since it was ungraded and informal, we took the opportunity to look at code from our Capstone project and grab examples that exhibited the qualities described by some anti-patterns. We also threw in some applicable jokes to emphasize the concepts of the patterns, and made a Kahoot quiz at the end to see if the other students were listening.