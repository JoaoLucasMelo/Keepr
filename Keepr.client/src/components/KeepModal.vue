<template>
  <div class="KeepModal">
    <div
      class="modal fade"
      :id="'a' + keep.id + 'a'"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
          <div class="modal-body row modalcard m-0">
            <div class="col-5 backpic p-0"></div>
            <div class="col-7">
              <div class="text-end">
                <button
                  type="button"
                  class="btn-close me-4 mt-4"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div class="d-flex heigth flex-column justify-content-between">
                <div>
                  <div class="row">
                    <div class="font d-flex justify-content-center">
                      <p class="d-flex align-items-center f-14 me-4">
                        <i
                          :title="'Viewed ' + keep.views + ' times'"
                          class="mdi mdi-24px mdi-eye-outline color me-2"
                        ></i>
                        {{ keep.views }}
                      </p>
                      <p class="d-flex align-items-center f-14">
                        <i
                          :title="'Kept ' + keep.keeps + ' times'"
                          class="mdi mdi-24px mdi-alpha-k-circle color me-2"
                        ></i>
                        {{ keep.keeps }}
                      </p>
                    </div>
                  </div>
                  <div class="row text-center">
                    <p class="keepname font m-0">{{ keep.name }}</p>
                    <p class="keepdesc font">{{ keep.description }}</p>
                  </div>
                </div>
                <div>
                  <div class="row justify-content-center py-3">
                    <div class="line"></div>
                  </div>
                  <div class="row">
                    <div class="mt-3 d-flex">
                      <div class="col-4 d-flex justify-content-center">
                        <button class="btn btn-outline-secondary">
                          Add to Vault
                        </button>
                      </div>
                      <div class="col-4 d-flex justify-content-center">
                        <i class="mdi mdi-24px mdi-trash-can"></i>
                      </div>
                      <div class="col-4 d-flex justify-content-center">
                        <i class="mdi mdi-24px mdi-trash-can"></i>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      imgUrl: computed(() => `url(${props.keep.img})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.modal-dialog {
  background-color: transparent;
}
.modal-content {
  background-color: transparent;
  border: 0;
}
.modalcard {
  background-color: white;
  height: 75vh;
  border-radius: 60px;
}
.backpic {
  background-image: v-bind(imgUrl);
  background-size: cover !important;
  border-bottom-left-radius: 60px;
  border-top-left-radius: 60px;
}
.modal-body {
  padding: 0;
}
.color {
  color: #e60023;
}
.keepname {
  font-size: 5vh;
  color: rgb(42, 42, 42);
}
.keepdesc {
  font-size: 3vh;
  color: rgb(42, 42, 42);
  padding-top: 5vh;
}
.line {
  margin: 5px;
  height: 0.8px;
  width: 90%;
  background: rgb(255, 255, 255);
  background: linear-gradient(
    90deg,
    rgba(255, 255, 255, 0) 0%,
    rgb(146, 146, 146) 25%,
    rgb(146, 146, 146) 75%,
    rgba(225, 225, 225, 0) 100%
  );
}
.heigth {
  height: 63vh;
}
</style>