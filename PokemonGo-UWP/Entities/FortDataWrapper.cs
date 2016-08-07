﻿using System.ComponentModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Google.Protobuf;
using PokemonGo_UWP.Utils;
using PokemonGo_UWP.Views;
using POGOProtos.Enums;
using POGOProtos.Map.Fort;
using Template10.Common;
using Template10.Mvvm;

namespace PokemonGo_UWP.Entities
{
    public class FortDataWrapper : IUpdatable<FortData>, INotifyPropertyChanged
    {
        private FortData _fortData;

        public FortDataWrapper(FortData fortData)
        {
            _fortData = fortData;
            Geoposition = new Geopoint(new BasicGeoposition { Latitude = _fortData.Latitude, Longitude = _fortData.Longitude });
        }

        public void Update(FortData update)
        {
            _fortData = update;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(ActiveFortModifier));
            OnPropertyChanged(nameof(CooldownCompleteTimestampMs));
            OnPropertyChanged(nameof(Enabled));
            OnPropertyChanged(nameof(GuardPokemonId));
            OnPropertyChanged(nameof(GymPoints));
            OnPropertyChanged(nameof(IsInBattle));
            OnPropertyChanged(nameof(LastModifiedTimestampMs));
            OnPropertyChanged(nameof(LureInfo));
            OnPropertyChanged(nameof(OwnedByTeam));
            OnPropertyChanged(nameof(RenderingType));
            OnPropertyChanged(nameof(Sponsor));
            OnPropertyChanged(nameof(Geoposition));
            OnPropertyChanged(nameof(GuardPokemonCp));
            OnPropertyChanged(nameof(Latitude));
            OnPropertyChanged(nameof(Longitude));
        }

        /// <summary>
        /// HACK - this should fix Pokestop floating on map
        /// </summary>
        public Point Anchor => new Point(0.5, 1);

        private DelegateCommand _trySearchPokestop;

        /// <summary>
        ///     We're just navigating to the capture page, reporting that the player wants to capture the selected Pokemon.
        ///     The only logic here is to check if the encounter was successful before navigating, everything else is handled by
        ///     the actual capture method.
        /// </summary>
        public DelegateCommand TrySearchPokestop => _trySearchPokestop ?? (
            _trySearchPokestop = new DelegateCommand(() =>
            {
                NavigationHelper.NavigationState["CurrentPokestop"] = this;
                BootStrapper.Current.NavigationService.Navigate(typeof(SearchPokestopPage), true);
            }, () => true)
            );

        #region Wrapped Properties

        public FortType Type => _fortData.Type;

        public ByteString ActiveFortModifier => _fortData.ActiveFortModifier;

        public long CooldownCompleteTimestampMs => _fortData.CooldownCompleteTimestampMs;

        public bool Enabled => _fortData.Enabled;

        public PokemonId GuardPokemonId => _fortData.GuardPokemonId;

        public long GymPoints => _fortData.GymPoints;

        public string Id => _fortData.Id;

        public bool IsInBattle => _fortData.IsInBattle;

        public long LastModifiedTimestampMs => _fortData.LastModifiedTimestampMs;

        public FortLureInfo LureInfo => _fortData.LureInfo;

        public TeamColor OwnedByTeam => _fortData.OwnedByTeam;

        public FortRenderingType RenderingType => _fortData.RenderingType;

        public FortSponsor Sponsor => _fortData.Sponsor;

        public Geopoint Geoposition { get; set; }

        public int GuardPokemonCp => _fortData.GuardPokemonCp;

        public double Latitude => _fortData.Latitude;

        public double Longitude => _fortData.Longitude;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
