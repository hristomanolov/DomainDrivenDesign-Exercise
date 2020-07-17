﻿using CarRentalSystem.Domain.Common;
using CarRentalSystem.Domain.Exceptions;
using CarRentalSystem.Domain.Models.CarAds;
using System.Collections.Generic;
using System.Linq;

using static CarRentalSystem.Domain.Models.ModelConstants.Common;

namespace CarRentalSystem.Domain.Models.Dealers
{
    public class Dealer : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<CarAd> carAds;

        internal Dealer(string name, string phoneNumber)
        {
            this.Validate(name);

            this.Name = name;
            this.PhoneNumber = phoneNumber;

            this.carAds = new HashSet<CarAd>();
        }

        private Dealer(string name)
        {
            this.Name = name;
            this.PhoneNumber = default!;

            this.carAds = new HashSet<CarAd>();
        }

        public string Name { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public Dealer UpdateName (string name)
        {
            this.Validate(name);
            this.Name = name;

            return this;
        }

        public Dealer UpdatePhoneNumber (string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;

            return this;
        }

        public IReadOnlyCollection<CarAd> CarAds
            => this.carAds.ToList().AsReadOnly();

        public void AddCarAd(CarAd carAd)
            => this.carAds.Add(carAd);

        private void Validate(string name)
            => Guard.ForStringLength<InvalidDealerException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
    }
}
