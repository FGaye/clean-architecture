
<template>
    <div class="flex justify-center py-16">
     <form action="" @submit.prevent="loginUser">
        <div class="flex justify-center">
          <h1>Sign In</h1>
        </div>
     <div>
        <label for="username">Username</label>
      <input type="text"  v-model="user.username" class="input input-bordered input-accent w-full max-w-xs ">
     </div>
    <div>
        <label for="password"> Password</label>
      <input type="password"  v-model="user.password" class="input input-bordered input-accent w-full max-w-xs ">
    </div>
      <div class="py-4">
        <button type="submit" class="btn btn-wide">sign in</button>
        <p>Don't' have an account? 
        <router-link :to="   { path: '/'}">
        <button class="btn btn-wide">sign up</button>
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
        username: '',
        password: '',
      },
      users: [],
    };
  },
  methods: {
    async loginUser(){
       let result = await axios.post("http://localhost:5123/Authentication/login", this.user)
        .then((response) => {
          this.users = response.data;
        this.$router.push({ path: '/movies' });
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