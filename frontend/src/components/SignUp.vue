<template>
  <div class="flex justify-center py-16">
   <form action="" @submit.prevent="createUser">
    <div class="flex justify-center">
            <h1>Sign Up</h1>
        </div>
   <div>
    <label for="full name">FullName</label>
    <input type="text" v-model="user.name" class="input input-bordered input-accent w-full max-w-xs ">
   </div>
  <div>
    <label for="email">Email</label>
    <input type="email" v-model="user.email"  class="input input-bordered input-accent w-full max-w-xs ">
  </div>
   <div>
    <label for="password">Password</label>
    <input type="password" v-model="user.password" class="input input-bordered input-accent w-full max-w-xs ">
   </div>
  <div class="py-4">
    <button type="submit" class="btn btn-wide">sign up</button>
    <p>Already have an account? 
        <router-link :to="{ path: '/signin'}">
            <button class="btn btn-wide">sign in</button>
        </router-link>
    
    </p>
  </div>
   </form>
</div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      user: {
        id: 0,
        name: '',
        email: '',
        password: '',
        
      },
      users: [],
    
      
    };
    
  },
  mounted() {
  
  },

  methods: {
    // fetchItem(movieId) {
    //   axios.get("http://localhost:5123/Authentication/register")
    //     .then(response => {
    //       this.movies = response.data;
          
    //     })
    //     .catch(error => {
    //       console.error(error);
    //     });
  
    async createUser(){
      //post a movie
       let result = await axios.post("http://localhost:5123/Authentication/register", this.user)
       //redirect to movie page
        .then((response) => {
          this.users = response.data;
          this.showAlert = true;
          this.alertMessage = 'registration successfully.';
          this.$router.push({name: 'signin'})


      setTimeout(() => {
        this.showAlert = false;
      }, 3000);
        
        })
      .catch((error) => 
      {
      if (error.response) {
        console.log(error.response.data);
        console.log(error.response.status);
        console.log(error.response.headers);
      } else if (error.request) {
        
        console.log(error.request);
      } else {
        
        console.log('Error', error.message);
      }
    });
      console.log(result)
    },
    }
   
  
};
</script>