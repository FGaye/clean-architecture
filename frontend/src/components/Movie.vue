<script>
import axios from "axios";
export default {
  data() {
    return {
      query: "",
      movies: [],
    };
  },
  mounted() {
    this.fetchMovies();
  },
  methods: {
    fetchMovies() {
      axios
        .get("http://localhost:5123/MovieApi/get")
        .then((response) => {
          this.movies = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
     async searchMovies() {
      if (!this.query) {
          return this.movies;
      }
      await axios.get( `http://localhost:5123/MovieApi?search=${this.query}`)
      .then( response => {
          this.movies = response.data;
      })
      .catch((error) => {console.log(error)})     
    },
    async searchMoviesForInputChange(e) {
      setTimeout(async () => {
        await this.searchMovies(this.query);
      }, 500);
    },
    deleteMovie(movieId)
    {
      if(confirm("Are you sure you want to delete this movie"))
      {
       axios.delete(`http://localhost:5123/MovieApi/delete?Id=${movieId}`).then(response => 
        {
          this.movies = this.movies.filter(movie => movie.id !== movieId);
        }).catch(error => {
          console.log(error);
        }
        )
      }
    }
  },
  
};

</script>
<template>
  <div class=" justify-center">
    <h1 class="flex justify-center py-4 text-lg text-bold">Movies</h1>
    <form @submit.prevent="searchMovies">
    <div class="flex justify-center py-8">
    <label for="search" ></label>
    <input type="search" placeholder="search movie" class="input input-bordered w-full max-w-xs" v-model="query" v-on:input="searchMoviesForInputChange"/>
    <button type="submit" class=" btn btn-primary" >Search</button>
    </div>
  </form>
    <div class="">
    <router-link to="Create">
      <button class="btn w-24 tooltip btn-success " data-tip="create a new movie ">Create</button>
    </router-link>
    </div>
    <div class="flex justify-center py-12 px-12 mx-12">
    <table class="table-md">
      <tr class="hover">
       <th>Title</th>
        <th>Description</th>
        <th>Price</th>
        <th>Genre</th>
        <th>Actions</th>
      </tr>
      <tr v-for="movie in movies" :key="movie.id" class="hover">
        <td>{{ movie.title }}</td>
        <td>{{ movie.description }}</td>
        <td>{{ movie.price }}</td>
        <td>{{ movie.genre }}</td>
        <td>
        <router-link :to="{path:`/${movie.id}/edit`}">
        <button class="btn w-24 tooltip btn-success" data-tip="edit movie ">Edit</button>
        </router-link>
        <button class="btn w-24 rounded-full btn-success" @click="deleteMovie(movie.id)">Delete</button>     
        </td>
      </tr>
    </table>
  </div>
  </div>
</template>

<style>
</style>
