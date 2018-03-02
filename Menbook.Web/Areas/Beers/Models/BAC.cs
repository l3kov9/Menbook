namespace Menbook.Web.Areas.Beers.Models
{
    public static class BAC
    {
        public static double Calculate(int age, int weight, double drinkingHours, int totalDrinks, double beerAbv)
        {
            var alcoholMass = totalDrinks * 0.5 * (beerAbv / 100.0 * 789.24);

            var bac = ((0.806 * alcoholMass * 1.2) / (0.58 * weight)) - (0.015 * drinkingHours);

            return bac;
        }

        public static CalculatorResultViewModel Conclusion(double bacResult)
        {
            var conclusion = string.Empty;
            var imageUrl = string.Empty;
            
            if (bacResult <= 0.01)
            {
                conclusion = "You're really not feeling the effects of alcohol right now.";
                imageUrl = "https://fthmb.tqn.com/mm0XORjQpYtckiap7AQsuklUkJM=/768x0/filters:no_upscale()/homer_2008_v2F_hires2-56a00fd43df78cafda9fde98.jpg";
            }
            else if (bacResult > 0.01 && bacResult <= 0.03)
            {
                conclusion = "You might feel a little more relaxed, perhaps a little light-headed. If you're shy you might lose a bit of your shyness. The typical depressant effects of alcohol are not affecting you at this stage and you probably haven’t lost any of your coordination.";
                imageUrl = "http://www.quotesforbros.com/wp-content/uploads/2015/10/Homer_Simpson_Vector_by_bark2008.png";
            }
            else if (bacResult > 0.03 && bacResult <= 0.06)
            {
                conclusion = "You're probably feeling a bit relaxed or you might have a feeling of well - being or euphoria (feeling really good or really positive). You might feel a bit warmer than you usually do. Your behavior might be a bit exaggerated or your emotions might be a bit intensified. You're probably feeling some impairment of reasoning and memory and since your level of caution is lower, it's a bad idea to drive.";
                imageUrl = "http://media.silabg.com/uf/common/homer_beer_muscle-1.jpg";
            }
            else if (bacResult > 0.06 && bacResult <= 0.09)
            {
                conclusion = "Although you can definitely tell you've been drinking, and you're probably aware that you're feeling some effects of alcohol, you might believe that you're more alert than you really are. In reality, with a BAC of 0.07 – 0.09 you'll be experiencing a slight impairment of speech, balance and hearing and your reaction time is reduced. Your caution, reasoning and memory are impaired, and your judgment and level of self-control are reduced.";
                imageUrl = "http://2.bp.blogspot.com/-0ohNNqTlk34/TcX29A7KqwI/AAAAAAAADOU/NPN68ZdFNhI/s1600/homerkeg.gif";
            }
            else if (bacResult > 0.09 && bacResult <= 0.12)
            {
                conclusion = "If your BAC is between 0.10 and 0.125 your speech will be slurred and your balance, vision, reactions time and hearing will be impaired. Your motor coordination will also be significantly impaired and you'll have a loss of good judgment.";
                imageUrl = "http://blog.campstay.com.au/wp-content/uploads/DuffBeerHomerSimpson.jpg";
            }
            else if (bacResult > 0.12 && bacResult <= 0.15)
            {
                conclusion = "Right now, you'd be feeling gross motor impairment and a lack of physical control. It'd probably be hard for you to type on the keyboard and your monitor or mobile phone screen would appear blurry. Your judgment and perception of what’s going on would be severely impaired and any feeling of euphoria (feeling really good or really positive) you had earlier will have turned to dysphoria (an emotional state of agitation, unease or depression). You'd have gross motor impairment and a lack of physical control.";
                imageUrl = "https://c1.staticflickr.com/5/4173/34455533212_b6444e506c_z.jpg";
            }
            else if (bacResult > 0.15 && bacResult <= 0.19)
            {
                conclusion = "You'll probably start to feel sick to your stomach. Any euphoria (feeling really good or really positive) you had earlier will have turned to dysphoria (an emotional state of agitation, unease or depression). Right now you might look like a sloppy drunk.";
                imageUrl = "https://www.kegworks.com/wp/wp-content/uploads/2013/05/HomerSimpson22.gif";
            }
            else if (bacResult > 0.19 && bacResult <= 0.21)
            {
                conclusion = "If your BAC is 0.20, you'll probably need help if you want to walk properly. If you fall down you probably won't feel a lot of pain – even if you hurt yourself. At this level of BAC, some people begin to vomit. At the very least, you'll feel dazed, confused and disoriented.";
                imageUrl = "http://img.timesnownews.com/zoom/story/1513186830-homer_beer.jpg?d=400x300";
            }
            else if (bacResult > 0.21 && bacResult <= 0.30)
            {
                conclusion = "If you were awake at this point you'd have very little comprehension as to where you were or what you were doing. You might pass out suddenly and it'd be hard to wake you up.";
                imageUrl = "http://www.freeduh.com/wp-content/uploads/2010/12/homer-simpson-with-111th-congress-tshirt-and-drunk-on-spending-beer.png";
            }
            else
            {
                conclusion = "It's possible that you might fall into a coma. You might die due to the paralysis of your diaphragm, the collapse of your lungs, or heart attack (any form of respiratory arrest).";
                imageUrl = "https://i.ebayimg.com/images/g/XtoAAOSwjk9ZTrlO/s-l300.jpg";
            }

            return new CalculatorResultViewModel
            {
                Conclusion = conclusion,
                ImageUrl = imageUrl
            };
        }
    }
}
