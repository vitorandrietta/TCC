﻿namespace Eletronicos.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A Interface to be used in DAO Classes
    /// </summary>
    /// <typeparam name="T">The model that represents a table row</typeparam>
    public interface IData<T>
    {
        /// <summary>
        /// insert a row ,represented by a generic object, in the correspondent table
        /// </summary>
        /// <param name="objectToBeInserted">a object representing the data to be inserted</param>
         void Insert(T objectToBeInserted);

        /// <summary>
        /// delete a row ,represented by a generic object, in the correspondent table
        /// </summary>
        /// <param name="objectToBeDeleted">a object representing the data to be deleted</param>
         void Delete(T objectToBeDeleted);

        /// <summary>
        /// update a row ,represented by a generic object, in the correspondent table
        /// </summary>
        /// <param name="objectToBeUpdated">a object representing the data to be updated</param>
         void Update(T objectToBeUpdated);

        /// <summary>
        /// Return all Registers of the correspondent table in the database
        /// </summary>
        /// <returns>a list of objects where which one corresponds to a row</returns>
         IList<T> FindAll();

        /// <summary>
        /// Returns the wanted object
        /// </summary>
        /// <param name="bjectToBeFound">A object containing the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter object</returns>
         T Find(T bjectToBeFound);

    }
}
