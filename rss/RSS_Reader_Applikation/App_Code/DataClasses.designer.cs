﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Database")]
public partial class DataClassesDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void Insertfeed(feed instance);
  partial void Updatefeed(feed instance);
  partial void Deletefeed(feed instance);
  #endregion
	
	public DataClassesDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<feed> feeds
	{
		get
		{
			return this.GetTable<feed>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.feeds")]
public partial class feed : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private int _feed_id;
	
	private string _feed_titel;
	
	private string _feed_url;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onfeed_idChanging(int value);
    partial void Onfeed_idChanged();
    partial void Onfeed_titelChanging(string value);
    partial void Onfeed_titelChanged();
    partial void Onfeed_urlChanging(string value);
    partial void Onfeed_urlChanged();
    #endregion
	
	public feed()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_feed_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public int feed_id
	{
		get
		{
			return this._feed_id;
		}
		set
		{
			if ((this._feed_id != value))
			{
				this.Onfeed_idChanging(value);
				this.SendPropertyChanging();
				this._feed_id = value;
				this.SendPropertyChanged("feed_id");
				this.Onfeed_idChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_feed_titel", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
	public string feed_titel
	{
		get
		{
			return this._feed_titel;
		}
		set
		{
			if ((this._feed_titel != value))
			{
				this.Onfeed_titelChanging(value);
				this.SendPropertyChanging();
				this._feed_titel = value;
				this.SendPropertyChanged("feed_titel");
				this.Onfeed_titelChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_feed_url", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
	public string feed_url
	{
		get
		{
			return this._feed_url;
		}
		set
		{
			if ((this._feed_url != value))
			{
				this.Onfeed_urlChanging(value);
				this.SendPropertyChanging();
				this._feed_url = value;
				this.SendPropertyChanged("feed_url");
				this.Onfeed_urlChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
#pragma warning restore 1591
