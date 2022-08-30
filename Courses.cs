using System;

namespace SportsManager
{
    class GolfCourse 
    {
        public int Rating = 73;
        public int Slope = 130;
        public int coursePar = 72;
        public int courseYardage = 7200;
        public int holeYardage = 400;
        public int holeHandicap = 7;
        public int holeNumber = 1;
        public int par = 4;
        public int getHoleYardage (int holeNumber)
        {
            return holeYardage;
        }
        public int getPar (int holeNumber) 
        {
            return par;
        }
        public int getCoursePar ()
        {
            return coursePar;
        }
        public int getCourseYardage ()
        {
            return courseYardage;
        }
    }
}