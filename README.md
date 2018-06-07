# Purpose
This repo isn't for keeping projects up-to-date; it's for showing off the projects I mostly enjoyed working on in college. Two projects didn't even have coding involved, and the one project that did is certainly nothing to brag about with regards to quality.

# Sliding Puzzle Solver
I worked on this project's back-end with one teammate, and the Unity front-end was done by a third teammate. The program solves any solvable 3x3 sliding puzzle using any of three algorithms. My favorite part about this project was being able to effectively debug somewhat-complicated algorithms through pair programming, and in the process of doing so, we first-hand experienced the value of using the right data structures. Our biggest performance increase went from solving a 31-step (that's the highest # steps) sliding puzzle in 15 minutes to solving it in several seconds, merely by changing a couple Lists into HashSets (we only needed to use those collections for checking if they contained specific values). A smaller chunk of time was shaved by changing some SortedLists to PriorityQueues (the collections were frequently accessed at the front and appended at the back). We were also given some freedom in choosing the three algorithms we wanted to use for traversing the state space; we ended up using Breadth-First, A*, and Greedy-Best-First algorithms.

# The Fundamentals of Reinforcement Learning


# Software Development Anti-Patterns