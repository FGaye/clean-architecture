<!-- create edit page to edit movies from the api and save -->
<script>
import axios from 'axios';
export default {
 
    data() {
      return {
        movieId:'',
        movie:{
        title: '',
        description: '',
        price: 0,
        genre: ''
      },
    }
  },
 mounted() {
  this.movieId = this.$route.params.id
  this.getMovieData(this.$route.params.id)
 },
methods:
 {
  getMovieData(movieId)
  {
    axios.get(`http://localhost:5123/MovieApi/edit/${movieId}`)
    .then(
       response => {
        this.movie = response.data
      }
    )
    .catch()
  },
  updateMovie()
  {
    axios.put(`http://localhost:5123/MovieApi/update/${this.movieId}`, this.movie)
    .then(
      response =>
      {
        this.movie = response.data
        this.$router.push({name: 'movies'})      
      }
    )
  }
 }
}
</script>



<template>
    <div class="flex justify-center py-4">
     <form @submit.prevent="updateMovie">
      <div>
          <router-link :to="{path: '/movies'}">
          <button class="btn btn-wide">back</button>
          </router-link>
        </div>
      <div class="grid  py-8 px-12">
        <h1 class="flex mx mx-12 py-4 text-lg text-bold ">Edit Page</h1>
      <div class="py-8">
      <label for="title">Title:</label>
      <input type="text" id="name" v-model="movie.title" required  class="input input-bordered input-accent w-full max-w-xs ">
      </div>

      <div class="mt-8">
      <label for="description">Description </label>
      <br>
      <textarea id="description" v-model="movie.description" class=" textarea textarea-bordered w-full  ">Description</textarea>
      </div>
      
      <div class="mt-8" >
      <label for="price" >Price</label>
      <input type="number"  id="price" v-model="movie.price"  class="input input-bordered input-accent w-full max-w-xs ">
      </div>

      <div class="mt-8" >
      <label for="genre">Genre</label>
      <input type="text"  id="genre" v-model="movie.genre"  class="input input-bordered input-accent w-full max-w-xs ">
      </div>

      <div class="mt-8 py-4">
      <button type="submit" class="btn btn-wide ">Save</button>
      </div>

      </div>
    </form>
    </div>

</template>

