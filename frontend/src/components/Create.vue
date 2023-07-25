<!-- create edit page to edit movies from the api and save -->
<script>
import axios from 'axios';

export default {
  data() {
    return {
      movie: {
        id: 0,
        title: '',
        description: '',
        price: 0,
        genre: '',
      },
      movies: [],
    }; 
  },
  mounted() {
    this.fetchItem();
  },
  methods: {
    fetchItem(movieId) {
      axios.get("http://localhost:5123/MovieApi/get-all-movies")
        .then(response => {
          this.movies = response.data;
          
        })
        .catch(error => {
          console.error(error);
        });
    },
    async createMovie(){
       let result = await axios.post("http://localhost:5123/MovieApi/create-movie", this.movie)
        .then((response) => {
          this.movies = response.data;
          this.$router.push({name: 'movies'})
        })
      .catch((error) => 
      {
     console.error(error);
    });
    },
    }
};
</script>



<template>
    <div class="flex justify-center py-4">
    <form @submit.prevent="createMovie">
      <div class="grid  py-8 px-12">
        <div>
          <router-link :to="{path: '/movies'}">
          <button class="btn btn-wide">back</button>
          </router-link>
        </div>
     <div class="flex justify-center">  <h1 class="text-xl text-bold">Create Movie</h1></div>
      <div class="py-8">
      <label for="title">Title</label>
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
      <button type="submit" class="btn btn-wide">Save</button>
      </div>

      </div>
    </form>
    </div>

</template>

