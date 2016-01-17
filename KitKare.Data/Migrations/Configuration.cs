namespace KitKare.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Server.Common;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    public sealed class Configuration : DbMigrationsConfiguration<KitKare.Data.KitKareDbContext>
    {
        private UserManager<User> userManager;
        private List<string> seededUsersIds = new List<string>();

        private byte[] defaultImageData;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(KitKareDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));

            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedTips(context);
            this.SeedVideo(context);
        }

        private void SeedVideo(KitKareDbContext context)
        {
            if (context.Videos.Any())
            {
                return;
            }

            var videoData = this.GetVideoData("video.mp4");

            var video = new Video
            {
                Data = videoData
            };

            context.Videos.Add(video);

            context.SaveChanges();
        }

        private byte[] GetVideoData(string videoFileName)
        {
            var directory = AssemblyHelpers.GetDirectoryForAssembly(Assembly.GetExecutingAssembly());
            var file = File.ReadAllBytes(directory + "/KitKare.Server/Assets/" + videoFileName);

            return file;
        }

        private void SeedTips(KitKareDbContext context)
        {
            if (context.CatCareTips.Any())
            {
                return;
            }

            var tip1 = new CatCareTip
            {
                Title = "Cat Equipment Basics: What You Need",
                Content = @"From collars and carriers to kitty litter and toys we take a look at all the equipment essentials you’ll need to care for your feline friend.
                
Bringing a new kitten home soon? How exciting! There’s nothing cuter than a playful, cuddly little furball of a kitten. Your kitten will quickly grow into an adult cat, and with a bit of luck, the two of you will be best friends for many years to come.

You can get the relationship off to a great start – and ease the nerves of both human and furball! – by making sure that you have everything your kitten will need.

Here’s a look at what you’ll need to have on hand when your kitten arrives.

KITTY LITTER

One of the perks of having a cat is that they’re inclined by nature to ‘go’ in the same area each time. No poop-scooping missions in your backyard will be necessary. But that means you’ll need to have a litter box for your cat.

There’s a wide range of options available. A litter box can be as simple as a plain plastic tray, or as complex as an automated poop-handler. Whatever works best for your budget and preferences will be fine.

But when it comes to the litter that goes in the box – well, your cat might have a say in what you use. If you notice your cat exhibiting some reluctance in using the litter box, it may be because of the litter. Try another brand. It might take experimenting with a few brands before kitty is content, but then all will be well.

BEDDING

Cats like to have a place to call their own for sleeping. When you’re shopping for a bed for your cat, you’ll find yourself faced with a huge array of choices. Whatever you buy should be soft and warm. It might be as simple as a pillow, but lots of cats show a preference for nest-type beds (usually shaped somewhat like a donut).

Of course, there’s a pretty decent chance that the cat will ultimately choose to sleep elsewhere – like in your bed with you. But still, it’s a good idea to have a kitty bed available for it.

SCRATCHING POSTS

Cats love to scratch. They need to scratch. And they WILL scratch; it’s instinctive! The only question is: What will your cat scratch? The smart move is to give your cat something that it will love to scratch, and that’s made for scratching.

That’s a scratching post.

As with most of the kitty equipment you’ll be buying, you’ll be able to choose from a multitude of brands and varieties. But here are a few tips for buying a just right scratching post for your pet:

- It should be sturdy and stable. A cat can hit a post hard in a vigorous play attack, so you want to be sure that the post will remain in place and upright. A rubber base might be helpful.
- It should be tall. In general, something in the range of 25 to 30 inches will probably be about right. It should be at least as tall as your full-grown cat’s reach when it’s standing on its hind legs.
- It should probably be vertical. Most cats like vertical scratching posts. But a few seem to prefer horizontal scratching posts. So if you find that your cat doesn’t seem inclined to play with its vertical post, you might need to trudge back to the store for a horizontal post.

FOOD AND WATER BOWLS

You’ll need food and water bowls for your cat, of course. Stainless steel bowls are the favorites among vets because they are easy to clean and sterilize. Glass and ceramic are also popular. Plastic bowls aren’t recommended because they may retain smells that the cat will find repellent. Plastic bowls are also more likely to be host to a kind of bacteria that can cause a type of feline acne.

Whatever the material of the bowl, make sure that it’s shallow in design – especially if your cat is still a small kitten. The cat will be more comfortable if it can easily reach the food.

TOYS, TOYS, TOYS

Kittens are playful critters, and playtime is an important component in their development. Cat toys are great playtime tools, and just about any purpose-built toy that your cat enjoys will be fine. Just be sure that the cat can’t get any small pieces off the toy. Anything smaller than a ping-pong ball should be considered a potential choking threat.

And don’t offer your hand to your kitten to swat at as a play toy. It might be cute now, but it won’t be when the cat is larger and has become conditioned to attacking your hands for fun!

GROOMING

You’ll want to keep your cat looking cute with the proper grooming equipment. At a minimum, you’ll want to have a metal-toothed comb (careful not to get one with sharp teeth), a bristle brush, and a flea comb.

If you have a longhaired cat, you’ll probably also need

- A wire brush (you can buy a wire/bristle combo brush if you wish)
- A metal-toothed comb with alternating long and short teeth
- A slicker brush
- A deshedder tool might also belong on your want list. Deshedders remove loose hairs before they can cause matting on the surface of the cat’s coat, and will make grooming duties considerably easier.

COLLAR AND CARRIER

While you’re not likely to be taking your cat out for a walk with a lead and collar, it’s still a good idea to have a collar for your cat. The collar should offer either ‘break away’ or ‘snap away’ features. Those safety features could prevent your cat from becoming helplessly stuck should its collar should become snagged on something. And of course you should have an identification tag on the collar.

Be sure to also buy a cat carrier. It’s the only safe way to transport your cat, even if you don’t plan any trips longer than to the nearest vet. A loose cat in a car is NOT a good thing! Again, you’ll be faced with a bewildering array of choices. Just make sure that the carrier you choose is well ventilated and sturdy.

ALL THE COMFORTS OF HOME

With a little bit of planning and careful shopping, you’ll have everything you need to make your house a happy home for your new furry friend. You’ll be laying the foundation for many years of happiness.
                ",
                CreatedOn = DateTime.Now
            };

            var tip2 = new CatCareTip
            {
                Title = "Cat Food: A Balanced Diet for Your Cat",
                Content = @"When it comes to food, your cat needs a balanced and nutritional diet as well as lots of fresh, clean water to stay happy and healthy.

If you’re a cat owner, you know that cats can be finicky eaters. But making sure that your cat gets a proper diet is important for good health and longevity. And cats do, of course, have their own particular dietary needs. Leftover dog food, milk from the fridge, or tuna from a can is not a proper diet.

But how much your cat eats is just as important is what your cat eats.

Perhaps the largest error cat owners make with their pet’s diet is in feeding too much. Many vets, in fact, say that obesity is the most prevalent nutritional disease seen in cats. ABC news has even reported that 1 of every 3 cats in Australia is obese. And the Australian Veterinary Association reports that pet obesity can lead to illnesses such as heart disease, diabetes, liver disease and arthritis.

So how much food does your cat need to avoid becoming a fat cat?

That depends upon a number of factors; you should get your vet’s recommendation for your cat. In general, though, a calorie intake of around 25 – 30 calories per pound of body weight per day is enough for most cats.

Kibble – dry cat food – is very popular among cat owners. After all, it’s an easy, no-fuss, no-muss method of feeding your cat. But dried cat food is rapidly becoming less popular with veterinarians.

The reason has to do with your cat’s genetic make-up. Mother Nature has designed cats to be meat eaters. And meat contains lots of water. So cats are less inclined to drink large quantities of water, because their proper diet is build around food that contains lots of liquid.

Canned cat food, on average, is about 70% water – about the same percentage of water that a meal of mouse would provide. And kibble is only about 10% water. So if you feed your cat primarily a diet of kibble, you could be setting it up for urinary tract problems down the road due to a lack of water in its diet.

That doesn’t mean that you shouldn’t feed your cat kibble at all. But it probably shouldn’t be the mainstay of your cat’s diet.

Whatever brand of food you feed your cat, it should be formulated specifically for cats. Cats have very specific dietary needs. Fall short on meeting any of those needs, and health problems may await your furry friend.

In recent years it’s become a popular trend among cat owners to prepare homemade cat food. It’s done with the best of intentions, of course. Well-meaning cat owners believe that the food they prepare for their pets is healthier and safer.

But mistakes are often made with homemade cat food that have unintended consequences. The meat in a cat’s diet, for example, must be properly balanced with calcium and phosphorous, duplicating the minerals that a cat would be getting from eating the bones of its prey in a natural diet.

Other common errors seen in homemade cat food formulation include:

- Too little meat. Could result in blindness, heart trouble – even death.
- Too much tuna. This can result in vitamin A toxicosis, possibly creating problems such as brittle bones, joint pain and dry skin.
- Too much raw fish. This can destroy vitamin B1, possibly resulting in muscular weakness or even brain damage.

Unless YOU are an expert, it’s best to leave the food formulation TO an expert.

Cats get most of their water from their diet – or should, anyway. But they can’t get all of their water from their food. And getting enough water is critical to your cat’s health. So you should always have clean, fresh water available for your pet to drink.

If you locate the water in locations where your cat likes to hang out, it will be more likely to drink its fill when needed. You might even consider distributing several water containers throughout the house, locating them in the cat’s favorite areas.

And just as they can be finicky eaters, some cats are also finicky drinkers. Chlorinated water, for example, may cause some cats to turn up their noses. If that’s your cat, try bottled water instead of tap water.

Keep an eye on how much water your cat drinks. If it appears that your cat is beginning to drink significantly more or less water than normal, alert your vet. Excessive water consumption, for example, could be an indication of illnesses like diabetes or hyperthyroidism.

Getting your cat’s diet right will pay great dividends. Instead of a sickly Garfield-like fat cat, you’ll have a lean and not-so-mean healthy cat.

Get your vet’s stamp of approval to be sure that you’re on the right track with your diet, and enjoy your furry friend for many years to come.",
                CreatedOn = DateTime.Now.AddDays(1)
            };

            var tip3 = new CatCareTip
            {
                Title = "Help my cat won’t eat",
                Content = @"If your cat is fussy and you need to feed a different diet, you may have encountered how much your cat dislikes change.

Many of us find it incredibly distressing when our cats won’t eat. Whether that be because your cat is a little unwell or needs a diet change, trying to encourage eating can be difficult. Many cats who develop kidney problems, diabetes or have food allergies may also need to eat a new diet. Introducing this new diet can mean you need to be a little bit sneaky. This can be compounded if your cat is also feeling a little poorly, particularly if you have a cat with kidney troubles.

If your cat is not feeling well, please visit your vet and get some specific advice and medical treatment. Many cats who are feeling unwell will develop food aversions and are then even more difficult to temp to eat even with home made cat food recipes. This article is not a substitute for proper medical attention if your cat is feeling poorly. If your vet has done everything they can and you are now left with a recovering cat who does not exactly have a ravenous appetite, hopefully we can give you a few hints.

Hopefully you have already visited your vet to get assistance with the medical reasons for not wanting to eat (mouth pain, nausea, viruses and gastrointestinal issues). Cats are creatures of habit and in an evolutionary sense they had no need to be experimental and adventurous with their foods, unlike our scavenging friends, the dogs. For cats, variety is not the spice of life. They would be perfectly happy eating that same old mouse every single day, they have no need for change. Some other basic principles to keep in mind when changing their food:

- Cats who have a blocked nose will often refuse to eat because they can’t smell their food. Heating the food to body temperature can make it smell nicer.
- Cats basically develop dietary preferences by 7 weeks of age, after their mum taught them what is food, and what is safe to eat. This is why changing foods in an adult cat is problematic.
- Cats will develop a preference for texture and appearance, so trying to make the new food look and feel like the old food can help the transition. For example if you want to introduce raw chicken necks, first cut them up into biscuit-sized pieces and sprinkle them with some crushed biscuit ‘seasoning’, then gradually leave the pieces bigger and bigger over time.
- Food aversions can also develop to something your cat has been eating if your cat was feeling a little sick last time the food was presented. Many ladies who have suffered morning sickness have experienced the same phenomenon, or perhaps this is the reason you can’t face Malibu and lemonade anymore!

A cat that is feeling nauseous will often lick his lips, salivate a lot or go over to the food bowl and walk away after having a sniff. Your vet may be able to help with medications for nausea and there are also appetite stimulants that can help while your pet is recovering. If a cat has developed a food aversion, trialling different foods can help. Usually it will take 2-4 weeks for your cat to be able to tolerate that food again. Ask your vet if there are different types of the food you can try, or what home-cooked diets are suitable.

Unless advised by your vet, all diet changes should be gradual, over at least 7 days. Your pet has enzymes and bacteria that have adapted to digest that old food, so a sudden diet change will cause diarrhoea and sometimes vomiting. During the first 3 days, add in 25% of the total volume, as the new food. The next two days, 50% should be the new food. On days 6-7, 75% of the total volume should be the new diet.

Cats are very sensitive to smell. They will often refuse to eat a new food until it is warmed to body temperature, instinctively they like to eat things ‘freshly killed’ and warm. Try microwaving the food until it is slightly warm, just watch for that hot spot in the middle and never feed cooked bones.

If you have a cat with urinary issues or diabetes your cat may be best eating a higher protein food or wet food. Many cats love dry biscuits, so transitioning them to wet food can result in a hunger strike. Follow the above advice with the gradual transition and start mixing your cat’s biscuits in with just a small amount of wet food. Adding some crushed up biscuits sprinkled on top can also help. You could start off with quite a lot of the extra ‘seasoning’, gradually reducing the amount over time.

The other option is to just wet down your cat’s biscuits, adding more and more water each day. Add some warm water to the biscuits and let them sit for around 20 minutes.

If your vet has advised you to start feeding your cat raw meat and bones for dental health, there are a few clever tricks that can encourage trying something new. Cats are often reluctant to eat meat that is cold and straight out of the fridge, so allow it to reach room temperature first. You can also sear the meat in a very hot pan, making sure just to put it on the heat for a few seconds, so the bone is still raw.

For an extra-sneaky tip, try cutting up the meat and bones into tiny pieces, crush up some dry food or mix it with some wet food. Aim to make it look as similar to your cat’s previous food as possible and mix the old and new foods together, gradually reducing the amount of the previous food over time.

ADDITIONAL SNEAKY TIPS FOR FUSSY EATERS:

- Some cats will eat if you pat them near their food bowl. Some nice long strokes from head to tail while near the food can in many cases stimulate their appetite.
- Encourage your cat to play, then present some nice fresh food just afterwards. A bit of exercise can stimulate appetite.
- Sometimes putting some food on your finger and allowing your cat to lick it off will get them started on the road to eating.
- Ask your vet what your cat is allowed to eat, and if BBQ chicken is suitable, perhaps try some BBQ chicken without the skin. This has a lovely fragrance and will tempt many fussy cats.
- Adding a probiotic to your cat’s food can improve palatability and also overall gut health and digestion. Ask your vet if this would be suitable for your cat.
- Add some salt reduced (onion-free) stock to your cat’s food as a flavouring. Cats actually aren’t sensitive to salt like we are, but that meaty broth can really add some flavour. You can do a similar thing with a little watered-down vegemite.",
                CreatedOn = DateTime.Now.AddDays(2)
            };

            context.CatCareTips.Add(tip1);
            context.CatCareTips.Add(tip2);
            context.CatCareTips.Add(tip3);

            context.SaveChanges();
        }

        private void SeedRoles(KitKareDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Admin"));
            context.SaveChanges();
        }

        private void SeedUsers(KitKareDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            this.SeedAdmin(context);
            this.SeedRegularUsers(context);
        }

        private void SeedAdmin(KitKareDbContext context)
        {
            var admin = new User
            {
                Email = "admin@admin.com",
                UserName = "admin"
            };

            this.userManager.Create(admin, "admin11");
            this.userManager.AddToRole(admin.Id, "Admin");

            context.SaveChanges();
        }

        private void SeedRegularUsers(KitKareDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Email = "user@user.com",
                    UserName = "user" + i
                };

                this.userManager.Create(user, "user11");
                seededUsersIds.Add(user.Id);
            }

            context.SaveChanges();
        }
    }
}
